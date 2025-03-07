from utils.api import play
from utils.constants import ERROR_COMMAND_FAILED
from utils.helpers import pretty_print_json

def execute():
    try:
        result = play()
        if result:
            pretty_print_json(result)
        else:
            print(ERROR_COMMAND_FAILED.format('play'))
    except Exception as e:
        print(f"An unexpected error occurred while executing 'play' command: {e}")

if __name__ == "__main__":
    execute()
