from utils.api import current_song
from utils.constants import ERROR_COMMAND_FAILED
from utils.sanitization import sanitize_input
from utils.helpers import pretty_print_json

def execute(index):
    try:
        result = current_song(index)
        if result:
            pretty_print_json(result)
        else:
            print(ERROR_COMMAND_FAILED.format('current'))
    except Exception as e:
        print(f"An unexpected error occurred while executing 'current' command: {e}")

if __name__ == "__main__":
    index = input("Enter the song index: ").strip()
    index = sanitize_input(index)
    execute(index)
