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
        join(slndir, "README.txt"),
        join(bindir, "TAUtil.dll"),
    ]

project_name = "snappymap"

tag = check_output(["git", "describe", "--dirty=-d"]).strip()

dist_name = project_name + "-" + tag
if not release_mode:
    dist_name += "-DEBUG"

zip_name = dist_name + ".zip"

dist_dir = dist_name

# build dist dir
if (os.path.exists(dist_dir)):
    shutil.rmtree(dist_dir)
os.mkdir(dist_dir)

for path in dist_files:
    shutil.copy(path, dist_dir)

# zip it up
zip_file = zipfile.ZipFile(zip_name, "w", zipfile.ZIP_DEFLATED)
for (dirpath, dirnames, filenames) in os.walk(dist_dir):
    for f in filenames:
        zip_file.write(join(dirpath, f))
zip_file.close()
