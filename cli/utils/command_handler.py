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
from commands.songs import execute as show_songs
from commands.ls import execute as list_titles

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
        current_song()
    elif command == "songs":
        show_songs()
    elif command == "ls":
        list_titles()
    elif command == "history":
        print_history()
    elif command == "about":
        display_about()
    elif command in ["cls", "clear"]:
        clear_screen()
    elif command == "help":
        print("\nTermina Commands:")
        print("  current    Show currently playing song")
        print("  songs      Full song list w/ current indicator") 
        print("  ls         Quick song titles list")
        print("  play       Play current/next song")
        print("  pause      Pause playback")
        print("  stop       Stop playback")
        print("  next       Next song")
        print("  previous   Previous song")
        print("  history    Command history")
        print("  about      Show Termina info")
        print("  cls/clear  Clear screen")
        print("  exit       Stop playback & quit\n")
    elif command == "exit":
        stop_playback_on_exit()
        print("Exiting Termina. Goodbye!")
        return False
    else:
        print("Unknown command. Type 'help' to see available commands.")
    
    add_to_history(command)
    return True
