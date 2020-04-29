all: run

run: 
    \bin\Debug\calc.exe

pack:

    mkdir "packdir"
    xcopy "uz_prirucka.pdf" to "packdir"
    xcopy "hodnoceni.txt" to "packdir"
    xcopy "odevzdani.txt" to "packdir"
    xcopy "install.exe" to "packdir"
    zip -r "xzedni15_xhanus17_xjulis00_xkruba00.zip" packdir
    rmdir "packdir"
    make clean
    
clean:
    

help:
    help.pdf
    

