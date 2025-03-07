from utils.api import stop
from utils.constants import ERROR_COMMAND_FAILED
from utils.helpers import pretty_print_json

def execute():
    try:
        result = stop()
        if result:
            pretty_print_json(result)
        else:
            print(ERROR_COMMAND_FAILED.format('stop'))
    except Exception as e:
        print(f"An unexpected error occurred while executing 'stop' command: {e}")

if __name__ == "__main__":
    execute()
