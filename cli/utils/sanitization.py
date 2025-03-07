import re
import html
import unicodedata

# Function to sanitize input against XSS attacks
def sanitize_xss(input_string):
    # Escape HTML characters
    return html.escape(input_string)

# Function to sanitize input against SQL Injection attacks
def sanitize_sql(input_string):
    # Replace common SQL injection characters and patterns
    sql_patterns = [
        re.compile(r'--'),   # SQL comment
        re.compile(r';'),    # SQL statement separator
        re.compile(r'\'"'),  # SQL single and double quotes
        re.compile(r'`'),    # SQL backtick
        re.compile(r'\bOR\b', re.IGNORECASE),  # SQL OR operator
        re.compile(r'\bAND\b', re.IGNORECASE)  # SQL AND operator
    ]
    sanitized_string = input_string
    for pattern in sql_patterns:
        sanitized_string = pattern.sub("", sanitized_string)
    return sanitized_string

# Function to sanitize input against code execution
def sanitize_code(input_string):
    # Disallow potentially dangerous characters or patterns
    code_patterns = [
        re.compile(r'<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>', re.IGNORECASE),
        re.compile(r'<[^>]+>'),  # HTML tags
        re.compile(r'&lt;[^&gt;]+&gt;'),  # HTML encoded tags
        re.compile(r'`'),  # Backticks
        re.compile(r'\$\('),  # Command substitution
        re.compile(r';'),  # Command chaining
        re.compile(r'\|\|'),  # Logical OR
        re.compile(r'\&\&'),  # Logical AND
        re.compile(r'\|'),  # Pipe
    ]
    sanitized_string = input_string
    for pattern in code_patterns:
        sanitized_string = pattern.sub("", sanitized_string)
    return sanitized_string

# Function to check if input is valid Unicode text
def is_valid_unicode(input_string):
    try:
        input_string.encode('utf-8').decode('utf-8')
        return True
    except UnicodeDecodeError:
        return False

# Function to remove non-Unicode characters
def remove_non_unicode(input_string):
    return ''.join(c for c in input_string if unicodedata.category(c) != 'Cn')

# Comprehensive input sanitization
def sanitize_input(input_string):
    # Check if input is valid Unicode text
    if not is_valid_unicode(input_string):
        raise ValueError("Input contains invalid Unicode characters.")
    
    # Remove non-Unicode characters
    input_string = remove_non_unicode(input_string)
    
    # Apply all sanitization functions
    input_string = sanitize_xss(input_string)
    input_string = sanitize_sql(input_string)
    input_string = sanitize_code(input_string)
    return input_string
