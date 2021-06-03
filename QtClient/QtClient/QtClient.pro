QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++11

unix: QMAKE_CXXFLAGS += -std=c++11
unix: QMAKE_LFLAGS_DEBUG += -std=c++11
win32-g++: QMAKE_CXXFLAGS += -std=c++0x

DEFINES += __STDC_LIMIT_MACROS

QGRPC_CONFIG = client

TEMPLATE = app

GRPC =  ../Protos/AppServer.proto
# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    main.cpp \
    MainWindow.cpp



HEADERS += \
    MainWindow.h

FORMS += \
    MainWindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target


INC_GRPC = ../grpc.pri
!include($${INC_GRPC}) {
                error("$$TARGET: File not found: $${INC_GRPC}")
                }
