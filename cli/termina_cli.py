import argparse
from ascii.intro import display_intro
from utils.logging_config import logger
from utils.sanitization import sanitize_input
from utils.command_handler import handle_command

def main():
    display_intro()  # Display the intro text first
    
    while True:
        try:
            command = input("Termina> ").strip().lower()
            command = sanitize_input(command)
            
            if not handle_command(command):
                break
        
        except ValueError as ve:
            print(f"Invalid input: {ve}")
            logger.error(f"Invalid input: {ve}")
        except KeyboardInterrupt:
            handle_command("exit")
            print("\nInterrupted. Exiting Termina. Goodbye!")
            logger.error("Interrupted by user.")
            break
        except Exception as e:
            print(f"An unexpected error occurred: {e}")
            logger.error(f"An unexpected error occurred: {e}")

if __name__ == "__main__":
    try:
        main()
    except Exception as e:
        handle_command("exit")
        print(f"Critical error: {e}")
        logger.critical(f"Critical error: {e}")
