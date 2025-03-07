import json

def pretty_print_json(data):
    try:
        print("\n", end="")  # Add a newline before the output
        if "message" in data:
            print(data["message"])
        elif "title" in data and "filePath" in data and "duration" in data and "artist" in data:
            print(f"Title: {data['title']}")
            print(f"FilePath: {data['filePath']}")
            print(f"Duration: {data['duration']}")
            print(f"Artist: {data['artist']}")
        else:
            print(json.dumps(data, indent=4, sort_keys=True))
        print("\n", end="")  # Add a newline after the output
    except (TypeError, ValueError) as e:
        print(f"Error processing response: {e}")
        print("\n", end="")  # Add a newline after the error message
