import sys


def to_unicode_escape(text):
    return "".join([f"\\u{ord(c):04x}" for c in text])


def main() -> None:
    assert len(sys.argv) > 1, (
        "You must provide a JP string to convert to a unicode escape sequence."
    )
    japanese_text = sys.argv[1]
    converted = to_unicode_escape(japanese_text)
    print(converted)


if __name__ == "__main__":
    main()
