from utils.api import current_song
from utils.constants import ERROR_COMMAND_FAILED

def execute():
    try:
        result = current_song()  # No index needed
        if result:
            print(f"Title: {result['title']}")
            print(f"FilePath: {result['filePath']}")
            print(f"Duration: {result['duration']}")
            print(f"Artist: {result['artist']}")
        else:
            # Instead of assuming failure, assume the player is stopped
            print("No song is currently playing.")
    except Exception as e:
        print(f"An unexpected error occurred while executing 'current' command: {e}")
