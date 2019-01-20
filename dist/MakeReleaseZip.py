from subprocess import check_output
import zipfile

import os
import os.path
from os.path import join
import sys
import shutil
import argparse

parser = argparse.ArgumentParser(description="Release packaging script")
parser.add_argument("--release", dest="release", action="store_const",
        const=True, default=False,
        help="package from release directory")
args = parser.parse_args()

release_mode = args.release

slndir = ".."
projdir = join(slndir, "SnappyMap")
if (release_mode):
    bindir = join(projdir, "bin/Release")
else:
    bindir = join(projdir, "bin/Debug")

dist_files = [
        join(bindir, "CommandLine.dll"),
        join(bindir, "HPIUtil.dll"),
        join(slndir, "LICENSE"),
        join(bindir, "SnappyMap.exe"),
        join(bindir, "SnappyMap.pdb"),
        join(slndir, "README.md"),
        join(bindir, "TAUtil.dll"),
        join(slndir, "Configs"),
    ]

project_name = "snappymap"

tag = check_output(["git", "describe", "--dirty=-d"], universal_newlines=True).strip()
version = tag
if not release_mode:
    version += "-DEBUG"

dist_name = project_name + "-" + version

zip_name = dist_name + ".zip"

dist_dir = dist_name

# build dist dir
if (os.path.exists(dist_dir)):
    shutil.rmtree(dist_dir)
os.mkdir(dist_dir)

for path in dist_files:
    if os.path.isdir(path):
        name = os.path.basename(path)
        shutil.copytree(path, os.path.join(dist_dir, name))
    else:
        shutil.copy(path, dist_dir)

# rename the readme
os.rename(join(dist_dir, "README.md"), join(dist_dir, "README.txt"))

# zip it up
zip_file = zipfile.ZipFile(zip_name, "w", zipfile.ZIP_DEFLATED)
for (dirpath, dirnames, filenames) in os.walk(dist_dir):
    for f in filenames:
        zip_file.write(join(dirpath, f))
zip_file.close()
