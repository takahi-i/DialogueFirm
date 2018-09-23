
/Applications/Unity/Unity.app/Contents/Mono/bin/smcs \
    -recurse:'Assets/SimpleBot/Library/*.cs' \
    -lib:/Applications/Unity/Unity.app/Contents/Managed/ \
    -r:UnityEngine \
    -r:UnityEditor \
    -target:library \
    -out:build/SimpleBot.dll
    
