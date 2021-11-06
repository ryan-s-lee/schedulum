ASEPRITE = "C:\aseprite\build\bin\aseprite.exe"
# find . -name "*.aseprite" -exec FILENAME=basename {} \; -exec "C:\aseprite\build\bin\aseprite.exe" -b {} --save-as $FILENAME \;
#find . -name "*.aseprite" -exec bash -c 'echo "$1"' _ {} "$(basename "{}")"\;
find . -name "*.aseprite" -exec bash -c '"C:\aseprite\build\bin\aseprite.exe" -b ${1} --save-as "$(basename ${1} .aseprite).png"' _ {} \;