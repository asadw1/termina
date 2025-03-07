from utils.api import previous_song
from utils.constants import ERROR_COMMAND_FAILED
from utils.helpers import pretty_print_json

def execute():
    try:
        result = previous_song()
        if result:
            pretty_print_json(result)
        else:
            print(ERROR_COMMAND_FAILED.format('previous'))
    except Exception as e:
        print(f"An unexpected error occurred while executing 'previous' command: {e}")

if __name__ == "__main__":
    execute()
