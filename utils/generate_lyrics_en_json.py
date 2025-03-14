import glob
import os

FILE_TEMPLATE = """
{{
    "BundleName": "lyrics_en",
    "Entries": [
{0}
    ]
}}
"""

ENTRY_TEMPLATE = """        {{
            "AssetsPath": "Assets/fata_unity/AssetBundleResources/data/lyrics_en/en/{0}",
            "BundlePath": "{1}"
        }}"""


def main() -> None:
    entries: list[str] = []

    for file in glob.glob(
        "Fata Morgana Remastered English Patch/Assets/fata_unity/AssetBundleResources/data/lyrics_en/en/*.png"
    ):
        filename = os.path.split(file)[1]
        filename_no_ext = os.path.splitext(filename)[0]
        entries.append(ENTRY_TEMPLATE.format(filename, filename_no_ext))

    print(FILE_TEMPLATE.format(",\n".join(entries)))


if __name__ == "__main__":
    main()
