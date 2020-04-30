all: run

run: 
    \bin\Debug\calc.exe

pack:

    mkdir packdir
    xcopy /s "uz_prirucka.pdf" packdir
    xcopy /s "hodnoceni.txt" packdir
    xcopy /s "odevzdani.txt" packdir
    xcopy /s "install.exe" packdir
    zip -r "xzedni15_xhanus17_xjulis00_xkruba00.zip" packdir
    rmdir packdir
    make clean
    
clean:
    

help:
    help.pdf
    

