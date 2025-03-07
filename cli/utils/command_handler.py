from commands.play import execute as play
from commands.pause import execute as pause
from commands.stop import execute as stop
from commands.next import execute as next_song
from commands.previous import execute as previous_song
from commands.current import execute as current_song
from commands.about import display_about
from commands.clear_screen import clear_screen
from utils.sanitization import sanitize_input
from utils.history import add_to_history, print_history

def stop_playback_on_exit():
    try:
        stop()
    except Exception as e:
        print(f"An unexpected error occurred while stopping playback: {e}")

def handle_command(command):
    if command == "play":
        play()
    elif command == "pause":
        pause()
    elif command == "stop":
        stop()
    elif command == "next":
        next_song()
    elif command == "previous":
        previous_song()
    elif command == "current":
        index = input("Enter the song index: ").strip()
        index = sanitize_input(index)
        current_song(index)
    elif command == "history":
        print_history()
    elif command == "about":
        display_about()
    elif command in ["cls", "clear"]:
        clear_screen()
    elif command == "help":
        print("Available commands: play, pause, stop, next, previous, current, history, about, cls, clear, help, exit")
    elif command == "exit":
        stop_playback_on_exit()
        print("Exiting Termina. Goodbye!")
        return False
    else:
        print("Unknown command. Type 'help' to see available commands.")
    
    add_to_history(command)
    return True
