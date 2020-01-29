
/Applications/Unity/Unity.app/Contents/Mono/bin/smcs \
    -recurse:'Assets/DialogFirm/Library/*.cs' \
    -lib:/Applications/Unity/Unity.app/Contents/Managed/ \
    -r:UnityEngine \
    -r:UnityEditor \
    -target:library \
    -out:Assets/DialogFirm/Lib/DialogueFirm.dll
