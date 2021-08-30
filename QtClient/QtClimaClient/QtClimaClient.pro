QT       += core gui network printsupport
QT      += xml
greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++11

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    ApplicationWorker.cpp \
    Controls/FanModeSwitch.cpp \
    Controls/FanWidget.cpp \
    Controls/QClickableLabel.cpp \
    Controls/qclickablelineedit.cpp \
    Controls/qcustomplot.cpp \
    Frames/Dialogs/MessageDialog.cpp \
    Frames/Dialogs/MinMaxByDayEditDialog.cpp \
    Frames/Dialogs/SelectProfileDialog.cpp \
    Frames/Dialogs/ValueByDayEditDialog.cpp \
    Frames/Dialogs/inputdigitdialog.cpp \
    Frames/Dialogs/inputtextdialog.cpp \
    Frames/FrameBase.cpp \
    Frames/Graphs/SelectProfileFrame.cpp \
    Frames/Graphs/TempProfileEditorFrame.cpp \
    Frames/LightConfigFrame.cpp \
    Frames/MainMenuFrame.cpp \
    Frames/ProductionFrame.cpp \
    Frames/SelectLightPresetFrame.cpp \
    Frames/SystemStateFrame.cpp \
    Frames/TemperatureConfigFrame.cpp \
    Frames/TemperatureOwerviewFrame.cpp \
    Frames/TestModeFrame.cpp \
    Frames/VentelationStateFrame.cpp \
    Frames/VentilationConfigFrame.cpp \
    Frames/VentilationOverviewFrame.cpp \
    GlobalContext.cpp \
    Models/Authorization/User.cpp \
    Models/Dialogs/ProfileInfoModel.cpp \
    Models/Dialogs/TempProfileModel.cpp \
    Models/LightTimersModel.cpp \
    Models/LivestockState.cpp \
    Network/ClientConnection.cpp \
    Network/GenericServices/GraphService.cpp \
    Network/GenericServices/LightControllerService.cpp \
    Network/GenericServices/SensorsService.cpp \
    Network/GenericServices/ServerInfoService.cpp \
    Network/GenericServices/SystemStatusService.cpp \
    Network/ReplyUserList.cpp \
    Network/RequestUserList.cpp \
    Network/Session.cpp \
    Services/DefaultFrameFactory.cpp \
    Services/FrameManager.cpp \
    TimerPool.cpp \
    main.cpp \
    MainWindow.cpp

HEADERS += \
    ApplicationWorker.h \
    Controls/FanControlsEnums.h \
    Controls/FanModeSwitch.h \
    Controls/FanWidget.h \
    Controls/QClickableLabel.h \
    Controls/qclickablelineedit.h \
    Controls/qcustomplot.h \
    Frames/Dialogs/MessageDialog.h \
    Frames/Dialogs/MinMaxByDayEditDialog.h \
    Frames/Dialogs/SelectProfileDialog.h \
    Frames/Dialogs/ValueByDayEditDialog.h \
    Frames/Dialogs/inputdigitdialog.h \
    Frames/Dialogs/inputtextdialog.h \
    Frames/FrameBase.h \
    Frames/Graphs/ProfileType.h \
    Frames/Graphs/SelectProfileFrame.h \
    Frames/Graphs/TempProfileEditorFrame.h \
    Frames/IFrameFactory.h \
    Frames/LightConfigFrame.h \
    Frames/MainMenuFrame.h \
    Frames/ProductionFrame.h \
    Frames/SelectLightPresetFrame.h \
    Frames/SystemStateFrame.h \
    Frames/TemperatureConfigFrame.h \
    Frames/TemperatureOwerviewFrame.h \
    Frames/TestModeFrame.h \
    Frames/VentelationStateFrame.h \
    Frames/VentilationConfigFrame.h \
    Frames/VentilationOverviewFrame.h \
    GlobalContext.h \
    MainWindow.h \
    Models/Authorization/User.h \
    Models/Dialogs/ProfileInfoModel.h \
    Models/Dialogs/TempProfileModel.h \
    Models/Graphs/MinMaxByDayPoint.h \
    Models/Graphs/MinMaxByDayProfile.h \
    Models/Graphs/ProfileInfo.h \
    Models/Graphs/ValueByDayPoint.h \
    Models/Graphs/ValueByDayProfile.h \
    Models/LightTimersModel.h \
    Models/LivestockState.h \
    Models/SensorsData.h \
    Models/SystemState.h \
    Network/AuthRequest.h \
    Network/ClientConnection.h \
    Network/GenericServices/GraphService.h \
    Network/GenericServices/LightControllerService.h \
    Network/GenericServices/Messages/ClimatStatusResponse.h \
    Network/GenericServices/Messages/CreateGraphRequest.h \
    Network/GenericServices/Messages/DefaultRequest.h \
    Network/GenericServices/Messages/GetProfileRequest.h \
    Network/GenericServices/Messages/GetProfileResponse.h \
    Network/GenericServices/Messages/GraphInfosRequest.h \
    Network/GenericServices/Messages/GraphInfosResponce.h \
    Network/GenericServices/Messages/LightPressetsResponse.h \
    Network/GenericServices/Messages/SensorsServiceRequest.h \
    Network/GenericServices/Messages/SensorsServiceResponse.h \
    Network/GenericServices/Messages/ServerInfoRequest.h \
    Network/GenericServices/Messages/TemperatureStateResponse.h \
    Network/GenericServices/Messages/UpdateValueByDayProfileRequest.h \
    Network/GenericServices/SensorsService.h \
    Network/GenericServices/ServerInfoService.h \
    Network/GenericServices/SystemStatusService.h \
    Network/INetworkService.h \
    Network/NetworkRequest.h \
    Network/NetworkResponse.h \
    Network/ReplyUserList.h \
    Network/Request.h \
    Network/RequestParams.h \
    Network/RequestUserList.h \
    Network/Session.h \
    Services/DefaultFrameFactory.h \
    Services/FrameManager.h \
    Services/FrameName.h \
    Services/QSerializer.h \
    TimerPool.h

FORMS += \
    Controls/FanModeSwitch.ui \
    Controls/qclickablelineedit.ui \
    Frames/Dialogs/MessageDialog.ui \
    Frames/Dialogs/MinMaxByDayEditDialog.ui \
    Frames/Dialogs/SelectProfileDialog.ui \
    Frames/Dialogs/ValueByDayEditDialog.ui \
    Frames/Dialogs/inputdigitdialog.ui \
    Frames/Dialogs/inputtextdialog.ui \
    Frames/Graphs/SelectProfileFrame.ui \
    Frames/Graphs/TempProfileEditorFrame.ui \
    Frames/LightConfigFrame.ui \
    Frames/MainMenuFrame.ui \
    Frames/ProductionFrame.ui \
    Frames/SelectLightPresetFrame.ui \
    Frames/SystemStateFrame.ui \
    Frames/TemperatureConfigFrame.ui \
    Frames/TemperatureOwerviewFrame.ui \
    Frames/TestModeFrame.ui \
    Frames/VentelationStateFrame.ui \
    Frames/VentilationConfigFrame.ui \
    Frames/VentilationOverviewFrame.ui \
    MainWindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target

DISTFILES += \
    Images/Fan.gif \
    Images/Industry-Automatic-icon.png \
    Images/Industry-Manual-icon.png \
    Images/add.png \
    Images/alert-icon.png \
    Images/auto.png \
    Images/backspace-arrow.png \
    Images/cancel-icon.png \
    Images/cancel.png \
    Images/chicken-128.png \
    Images/chicken-32.png \
    Images/circled-down.png \
    Images/circled-left.png \
    Images/circled-up.png \
    Images/delete.png \
    Images/exclamation.png \
    Images/fan-off.png \
    Images/fan-on.png \
    Images/gears.png \
    Images/icon-manual-27.jpg \
    Images/icons8-add-property-50.png \
    Images/icons8-double-right-48.png \
    Images/icons8-edit-64.png \
    Images/icons8-remove-64.png \
    Images/info.png \
    Images/line-chart.png \
    Images/ok.png \
    Images/power-button-off.png \
    Images/remove.png \
    Images/save.png

RESOURCES += \
    Resources.qrc
