QT       += core gui network
QT      += xml
greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++11

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    Frames/Dialogs/AuthorizationDialog.cpp \
    Frames/FrameBase.cpp \
    Frames/MainMenuFrame.cpp \
    Frames/SystemStateFrame.cpp \
    Frames/VentelationStateFrame.cpp \
    Models/Authorization/User.cpp \
    Models/LivestockState.cpp \
    Network/ClientConnection.cpp \
    Network/ReplyUserList.cpp \
    Network/RequestUserList.cpp \
    Network/Session.cpp \
    Services/DefaultFrameFactory.cpp \
    Services/FrameManager.cpp \
    main.cpp \
    MainWindow.cpp

HEADERS += \
    Frames/Dialogs/AuthorizationDialog.h \
    Frames/FrameBase.h \
    Frames/IFrameFactory.h \
    Frames/MainMenuFrame.h \
    Frames/SystemStateFrame.h \
    Frames/VentelationStateFrame.h \
    MainWindow.h \
    Models/Authorization/User.h \
    Models/LivestockState.h \
    Network/AuthRequest.h \
    Network/ClientConnection.h \
    Network/NetworkReply.h \
    Network/NetworkRequest.h \
    Network/ReplyUserList.h \
    Network/Request.h \
    Network/RequestParams.h \
    Network/RequestUserList.h \
    Network/Session.h \
    Services/DefaultFrameFactory.h \
    Services/FrameManager.h \
    Services/FrameName.h \
    Services/QSerializer.h

FORMS += \
    Frames/Dialogs/AuthorizationDialog.ui \
    Frames/MainMenuFrame.ui \
    Frames/SystemStateFrame.ui \
    Frames/VentelationStateFrame.ui \
    MainWindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target
