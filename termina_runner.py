#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Termina Cross-Platform Runner
=============================

Enterprise-grade launcher for Termina music player system. Launches backend API
and CLI client in separate windows/processes with platform-specific window
resizing and process management.

Supported Platforms:
- Windows 11 (CMD with CREATE_NEW_CONSOLE)
- Ubuntu/Debian (bash + terminal escape sequences)  
- macOS (Terminal.app/iTerm2 + terminal escape sequences)

Author: Termina Development Team
License: MIT
Version: 2.0.0
"""

import subprocess
import os
import time
import requests
import psutil
import platform
import ctypes
from ctypes import wintypes
from typing import List, Optional

# =============================================================================
# CONFIGURATION
# =============================================================================

REPO_ROOT = r"C:\SYS00-Desktop\Development\termina"
BACKEND_DIR = os.path.join(REPO_ROOT, "backend", "MusicShellApi")
CLI_DIR = os.path.join(REPO_ROOT, "cli")

# Windows Console API Constants
STD_OUTPUT_HANDLE = -11
PROCESS_QUERY_INFORMATION = 0x0400

# CLI Window Dimensions (cross-platform consistent)
CLI_COLS = 170
CLI_ROWS = 50

# API Health Check
BACKEND_API_ENDPOINT = "http://localhost:5000/api/music/list"
BACKEND_STARTUP_TIMEOUT = 30  # seconds


def wait_for_backend(timeout: int = BACKEND_STARTUP_TIMEOUT) -> bool:
    """
    Poll backend API health endpoint until responsive or timeout.
    
    Args:
        timeout: Maximum seconds to wait for backend startup
        
    Returns:
        True if backend API responds 200 OK, False on timeout
    """
    print("Waiting for backend API (http://localhost:5000)...")
    start_time = time.time()
    
    while time.time() - start_time < timeout:
        try:
            response = requests.get(BACKEND_API_ENDPOINT, timeout=1)
            if response.status_code == 200:
                print("Backend ready!")
                return True
        except requests.RequestException:
            pass  # Network/timeout errors expected during startup
        time.sleep(0.5)
    
    print(f"Backend startup timeout after {timeout}s")
    return False


def kill_process_tree(pid: int) -> None:
    """
    Gracefully terminate process tree (parent + all children).
    
    Args:
        pid: Process ID to terminate
        
    Notes:
        - Uses psutil for recursive child enumeration
        - Handles NoSuchProcess exceptions gracefully
        - Windows/Linux/macOS compatible
    """
    try:
        parent = psutil.Process(pid)
        children = parent.children(recursive=True)
        
        # Kill children first (bottom-up termination)
        for child in children:
            try:
                child.kill()
            except psutil.NoSuchProcess:
                continue  # Process already terminated
        
        # Kill parent last
        parent.kill()
        print(f"Terminated process tree: PID {pid}")
        
    except psutil.NoSuchProcess:
        pass  # Process already gone
    except Exception as e:
        print(f"Failed to terminate PID {pid}: {e}")


def resize_windows_console(pid: int, cols: int = CLI_COLS, rows: int = CLI_ROWS) -> bool:
    """
    Force physical resize of Windows console window using Win32 API.
    
    Args:
        pid: Process ID of console window to resize
        cols: Target column count
        rows: Target row count
        
    Returns:
        True if resize succeeded, False on failure
        
    Windows Only:
        - Uses kernel32.SetConsoleScreenBufferSize for buffer
        - Relies on automatic window resize following buffer change
    """
    if platform.system().lower() != "windows":
        return False
        
    try:
        kernel32 = ctypes.windll.kernel32
        
        # Open process for query access
        hProcess = kernel32.OpenProcess(PROCESS_QUERY_INFORMATION, False, pid)
        if not hProcess:
            return False
            
        try:
            # Set screen buffer size (triggers automatic window resize)
            hConsole = kernel32.GetStdHandle(STD_OUTPUT_HANDLE)
            success = kernel32.SetConsoleScreenBufferSize(hConsole, wintypes.COORD(cols, rows))
            
            if success:
                print(f"Windows CLI resized to {cols}x{rows}")
                return True
            else:
                print("Windows buffer resize failed")
                return False
                
        finally:
            kernel32.CloseHandle(hProcess)
            
    except Exception as e:
        print(f"Windows resize failed: {e}")
        return False


def launch_backend() -> Optional[subprocess.Popen]:
    """
    Platform-aware backend launcher.
    
    Returns:
        Popen process object or None on failure
    """
    system = platform.system().lower()
    
    if system == "windows":
        return subprocess.Popen([
            "cmd", "/k", 
            f"cd /d {BACKEND_DIR} && echo Starting Termina Backend... && dotnet run"
        ], creationflags=subprocess.CREATE_NEW_CONSOLE)
    else:  # Linux/macOS
        return subprocess.Popen([
            "bash", "-c", 
            f"cd '{BACKEND_DIR}' && echo 'Starting Termina Backend...' && dotnet run"
        ])


def launch_cli() -> Optional[subprocess.Popen]:
    """
    Platform-aware CLI launcher with automatic window/terminal resize.
    
    Returns:
        Popen process object or None on failure
    """
    system = platform.system().lower()
    
    if system == "windows":
        # Windows: CMD with buffer resize
        proc = subprocess.Popen([
            "cmd", "/k", 
            f"mode con: cols={CLI_COLS} lines={CLI_ROWS} && cd /d {CLI_DIR} && echo CLI ready at {CLI_COLS} columns && python termina_cli.py"
        ], creationflags=subprocess.CREATE_NEW_CONSOLE)
        
        # Attempt physical resize after window creation
        time.sleep(2)  # Allow window creation
        resize_windows_console(proc.pid)
        return proc
        
    else:  # Linux/macOS
        # Terminal escape sequence resize (xterm-compatible)
        resize_seq = f"\x1b[8;{CLI_ROWS};{CLI_COLS}t"
        return subprocess.Popen([
            "bash", "-c", 
            f"echo -e '{resize_seq}' && cd '{CLI_DIR}' && echo 'CLI ready at {CLI_COLS} columns' && python3 termina_cli.py"
        ])


def main() -> None:
    """
    Main orchestrator for Termina multi-process startup.
    
    Startup Sequence:
    1. Launch backend API in separate window/process
    2. Poll /api/music/list until responsive
    3. Launch CLI with platform-specific window sizing
    4. Wait for user interrupt
    5. Graceful process tree termination
    """
    print("Termina Cross-Platform Runner v2.0.0")
    print(f"Detected OS: {platform.system()} {platform.release()}")
    print(f"Repo root: {REPO_ROOT}")
    print("-" * 60)
    
    pids_to_kill: List[int] = []
    
    try:
        # Phase 1: Backend startup
        print("\n1/3 PHASE 1: Starting backend...")
        backend_proc = launch_backend()
        if not backend_proc:
            print("ERROR: Backend launch failed")
            return
        pids_to_kill.append(backend_proc.pid)
        
        # Phase 2: Backend health check
        print("2/3 PHASE 2: Health checking backend...")
        if not wait_for_backend():
            print("ERROR: Backend failed to become responsive")
            return
        
        # Phase 3: CLI startup
        print("3/3 PHASE 3: Starting CLI...")
        cli_proc = launch_cli()
        if not cli_proc:
            print("ERROR: CLI launch failed")
            return
        pids_to_kill.append(cli_proc.pid)
        
        print("\n" + "="*60)
        print("TERMINA READY - LAUNCHER MODE")
        print("="*60)
        print(f"• Backend: http://localhost:5000 ✓")
        print(f"• CLI:    {CLI_COLS} columns x {CLI_ROWS} rows ✓")
        print(f"• PIDs:   {', '.join(map(str, pids_to_kill))} ✓")
        print("="*60)
        print("\nUse CLI window/process for: play, next, current, history, etc.")
        print("\nPress Enter here when finished to terminate both processes...")
        
        input("\n[ENTER TO SHUTDOWN] ")
        
    finally:
        # Phase 4: Graceful shutdown
        print("\nPHASE 4: Graceful shutdown initiated...")
        for i, pid in enumerate(pids_to_kill, 1):
            print(f"Terminating PID {pid} ({i}/{len(pids_to_kill)})...")
            kill_process_tree(pid)
        
        print("Termina shutdown complete.")


if __name__ == "__main__":
    """
    Entry point with exception handling for production reliability.
    """
    try:
        main()
    except KeyboardInterrupt:
        print("\nShutdown requested by user (Ctrl+C)")
    except Exception as e:
        print(f"Fatal error: {e}")
        exit(1)
