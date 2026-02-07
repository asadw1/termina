from utils.api import list_songs
from utils.api import current_song
from utils.constants import ERROR_COMMAND_FAILED

def execute():
    try:
        titles = list_songs()
        current = current_song()
        current_index = None
        
        # Find current song index by title match
        if current and titles:
            for i, title in enumerate(titles):
                if title == current['title']:
                    current_index = i
                    break
        
        if titles:
            print(f"{len(titles)} songs:{' LIVE' if current else ''}")
            for i, title in enumerate(titles):
                marker = "▶ " if i == current_index else "  "
                print(f"   {marker}{i:2d}. {title}")
        else:
            print("No songs found")
    except Exception as e:
        print(f"Error: {e}")
