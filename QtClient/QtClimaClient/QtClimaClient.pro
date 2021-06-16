QT       += core gui network
QT      += xml
greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++11

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    Frames/FrameBase.cpp \
    Frames/MainMenuFrame.cpp \
    Frames/SystemStateFrame.cpp \
    Frames/VentelationStateFrame.cpp \
    Network/ClientConnection.cpp \
    Services/DefaultFrameFactory.cpp \
    Services/FrameManager.cpp \
    main.cpp \
    MainWindow.cpp

HEADERS += \
    Frames/FrameBase.h \
    Frames/IFrameFactory.h \
    Frames/MainMenuFrame.h \
    Frames/SystemStateFrame.h \
    Frames/VentelationStateFrame.h \
    MainWindow.h \
    Network/ClientConnection.h \
    Network/NetworkReply.h \
    Network/NetworkRequest.h \
    Services/DefaultFrameFactory.h \
    Services/FrameManager.h \
    Services/QSerializer.h

FORMS += \
    Frames/MainMenuFrame.ui \
    Frames/SystemStateFrame.ui \
    Frames/VentelationStateFrame.ui \
    MainWindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target
