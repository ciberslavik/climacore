QT       += core gui network printsupport
QT      += xml
greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++11

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    ApplicationWorker.cpp \
    Controls/qcustomplot.cpp \
    Frames/FrameBase.cpp \
    Frames/MainMenuFrame.cpp \
    Frames/SystemStateFrame.cpp \
    Frames/VentelationStateFrame.cpp \
    GlobalContext.cpp \
    Models/Authorization/User.cpp \
    Models/LivestockState.cpp \
    Network/ClientConnection.cpp \
    Network/GenericServices/GraphService.cpp \
    Network/GenericServices/SensorsService.cpp \
    Network/GenericServices/ServerInfoService.cpp \
    Network/GenericServices/SystemStatusService.cpp \
    Network/ReplyUserList.cpp \
    Network/RequestUserList.cpp \
    Network/Session.cpp \
    Services/DefaultFrameFactory.cpp \
    Services/FrameManager.cpp \
    main.cpp \
    MainWindow.cpp

HEADERS += \
    ApplicationWorker.h \
    Controls/qcustomplot.h \
    Frames/FrameBase.h \
    Frames/IFrameFactory.h \
    Frames/MainMenuFrame.h \
    Frames/SystemStateFrame.h \
    Frames/VentelationStateFrame.h \
    GlobalContext.h \
    MainWindow.h \
    Models/Authorization/User.h \
    Models/LivestockState.h \
    Models/SensorsData.h \
    Models/SystemState.h \
    Network/AuthRequest.h \
    Network/ClientConnection.h \
    Network/GenericServices/GraphService.h \
    Network/GenericServices/Messages/GraphInfosResponce.h \
    Network/GenericServices/Messages/SensorsServiceRequest.h \
    Network/GenericServices/Messages/SensorsServiceResponse.h \
    Network/GenericServices/Messages/ServerInfoRequest.h \
    Network/GenericServices/SensorsService.h \
    Network/GenericServices/ServerInfoService.h \
    Network/GenericServices/SystemStatusService.h \
    Network/INetworkService.h \
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
    Frames/MainMenuFrame.ui \
    Frames/SystemStateFrame.ui \
    Frames/VentelationStateFrame.ui \
    MainWindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target
