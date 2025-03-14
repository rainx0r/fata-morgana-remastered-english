import re

OMAKE_PATH = "Fata Morgana Remastered English Patch/Assets/fata_unity/AssetBundleResources/data/patch/data/scenario"
OMAKE_PATH_EN = OMAKE_PATH + "/ks_en/omake.txt"

exp = re.compile(
    r"[\u3000-\u303f\u3040-\u309f\u30a0-\u30ff\uff00-\uff9f\u4e00-\u9faf\u3400-\u4dbf]+"
)

with open(OMAKE_PATH_EN, encoding="utf-8") as f:
    omake_en = f.readlines()

for i, line in enumerate(omake_en):
    line = re.sub(r"\[.*?\]", "", line)
    line = re.sub(r"【.*?】", "", line)
    if line.startswith("*") or line.startswith(";"):
        continue
    if exp.findall(re.sub(r"\[.*?\]", "", line)):
        print(i, line)
