#include "MainWindow.h"

#include <QApplication>
#include <QFile>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);


    QFile green_led("/sys/class/leds/agava:programm:green/brightness");
    if(green_led.open(QFile::OpenModeFlag::WriteOnly))
    {
        green_led.write("1", 1);
        green_led.close();
    }
    MainWindow w;
    w.show();
    return a.exec();
}
