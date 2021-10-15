QT       += core gui network printsupport
QT      += xml sql
greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++11
#QMAKE_CXXFLAGS += -Wno-deprecated-copy
# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    ApplicationWorker.cpp \
    Controls/AnalogModeSwitch.cpp \
    Controls/FanModeSwitch.cpp \
    Controls/FanWidget.cpp \
    Controls/QClickableLabel.cpp \
    Controls/qclickablelineedit.cpp \
    Controls/qcustomplot.cpp \
    Frames/Dialogs/ConfigValveDialog.cpp \
    Frames/Dialogs/EditAnalogFanDialog.cpp \
    Frames/Dialogs/EditFanDialog.cpp \
    Frames/Dialogs/HeaterConfigDialog.cpp \
    Frames/Dialogs/LivestockOperationDialog.cpp \
    Frames/Dialogs/MessageDialog.cpp \
    Frames/Dialogs/MinMaxByDayEditDialog.cpp \
    Frames/Dialogs/StartPreparingDialog.cpp \
    Frames/Dialogs/StartProductionDialog.cpp \
    Frames/Dialogs/ValueByDayEditDialog.cpp \
    Frames/Dialogs/ValueByValueEditDialog.cpp \
    Frames/Dialogs/inputdigitdialog.cpp \
    Frames/Dialogs/inputtextdialog.cpp \
    Frames/FrameBase.cpp \
    Frames/Graphs/ConfigProfilesFrame.cpp \
    Frames/Graphs/SelectProfileFrame.cpp \
    Frames/Graphs/TempProfileEditorFrame.cpp \
    Frames/Graphs/ValveProfileEditorFrame.cpp \
    Frames/Graphs/VentProfileEditorFrame.cpp \
    Frames/HeaterControllerFrame.cpp \
    Frames/Hystory/TemperatureHystoryFrame.cpp \
    Frames/HystoryMenuFrame.cpp \
    Frames/LightConfigFrame.cpp \
    Frames/MainMenuFrame.cpp \
    Frames/ProductionFrame.cpp \
    Frames/SelectLightPresetFrame.cpp \
    Frames/SystemStateFrame.cpp \
    Frames/TemperatureConfigFrame.cpp \
    Frames/TemperatureOwerviewFrame.cpp \
    Frames/TestModeFrame.cpp \
    Frames/VentControllerConfigFrame.cpp \
    Frames/VentelationStateFrame.cpp \
    Frames/VentilationConfigFrame.cpp \
    Frames/VentilationOverviewFrame.cpp \
    GlobalContext.cpp \
    Models/Authorization/User.cpp \
    Models/Dialogs/ProfileInfoModel.cpp \
    Models/Dialogs/TempProfileModel.cpp \
    Models/Dialogs/ValveProfileModel.cpp \
    Models/Dialogs/VentProfileModel.cpp \
    Models/FanInfosModel.cpp \
    Models/LightTimersModel.cpp \
    Network/ClientConnection.cpp \
    Network/GenericServices/DeviceProviderService.cpp \
    Network/GenericServices/GraphService.cpp \
    Network/GenericServices/HeaterControllerService.cpp \
    Network/GenericServices/LightControllerService.cpp \
    Network/GenericServices/LivestockService.cpp \
    Network/GenericServices/ProductionService.cpp \
    Network/GenericServices/SchedulerControlService.cpp \
    Network/GenericServices/SensorsService.cpp \
    Network/GenericServices/ServerInfoService.cpp \
    Network/GenericServices/SystemStatusService.cpp \
    Network/GenericServices/VentilationService.cpp \
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
    Controls/AnalogModeSwitch.h \
    Frames/Dialogs/EditAnalogFanDialog.h \
    Frames/Dialogs/EditFanDialog.h \
    Frames/Dialogs/HeaterConfigDialog.h \
    Frames/Dialogs/StartPreparingDialog.h \
    Frames/Dialogs/StartProductionDialog.h \
    Frames/Dialogs/ValueByValueEditDialog.h \
    Frames/Graphs/ConfigProfilesFrame.h \
    Frames/Graphs/ValveProfileEditorFrame.h \
    Frames/Graphs/VentProfileEditorFrame.h \
    Frames/HeaterControllerFrame.h \
    Frames/Hystory/TemperatureHystoryFrame.h \
    Frames/HystoryMenuFrame.h \
    Frames/VentControllerConfigFrame.h \
    Models/Dialogs/ValveProfileModel.h \
    Models/Dialogs/VentProfileModel.h \
    Models/FanControlsEnums.h \
    Controls/FanModeSwitch.h \
    Controls/FanWidget.h \
    Controls/QClickableLabel.h \
    Controls/qclickablelineedit.h \
    Controls/qcustomplot.h \
    Frames/Dialogs/ConfigValveDialog.h \
    Frames/Dialogs/LivestockOperationDialog.h \
    Frames/Dialogs/MessageDialog.h \
    Frames/Dialogs/MinMaxByDayEditDialog.h \
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
    Models/FanInfo.h \
    Models/FanInfosModel.h \
    Models/Graphs/MinMaxByDayPoint.h \
    Models/Graphs/MinMaxByDayProfile.h \
    Models/Graphs/ProfileInfo.h \
    Models/Graphs/ValueByDayPoint.h \
    Models/Graphs/ValueByDayProfile.h \
    Models/Graphs/ValueByValuePoint.h \
    Models/Graphs/ValueByValueProfile.h \
    Models/HeaterParams.h \
    Models/HeaterState.h \
    Models/LightTimersModel.h \
    Models/LivestockState.h \
    Models/PreparingConfig.h \
    Models/ProductionConfig.h \
    Models/ProductionState.h \
    Models/RelayInfo.h \
    Models/SchedulerInfo.h \
    Models/SchedulerProcessInfo.h \
    Models/SchedulerProfilesInfo.h \
    Models/SensorsData.h \
    Models/SystemState.h \
    Models/VentControllerState.h \
    Models/VentilationParams.h \
    Network/AuthRequest.h \
    Network/ClientConnection.h \
    Network/GenericServices/DeviceProviderService.h \
    Network/GenericServices/GraphService.h \
    Network/GenericServices/HeaterControllerService.h \
    Network/GenericServices/LightControllerService.h \
    Network/GenericServices/LivestockService.h \
    Network/GenericServices/Messages/ClimatStatusResponse.h \
    Network/GenericServices/Messages/CreateGraphRequest.h \
    Network/GenericServices/Messages/DefaultRequest.h \
    Network/GenericServices/Messages/FanInfoListRsponse.h \
    Network/GenericServices/Messages/FanInfoRequest.h \
    Network/GenericServices/Messages/FanKeyRequest.h \
    Network/GenericServices/Messages/FanModeRequest.h \
    Network/GenericServices/Messages/FanModeResponse.h \
    Network/GenericServices/Messages/FanRemoveRequest.h \
    Network/GenericServices/Messages/FanStateRequest.h \
    Network/GenericServices/Messages/FanStateResponse.h \
    Network/GenericServices/Messages/GetProfileRequest.h \
    Network/GenericServices/Messages/GetTempProfileResponse.h \
    Network/GenericServices/Messages/GetValveProfileResponse.h \
    Network/GenericServices/Messages/GetVentProfileResponse.h \
    Network/GenericServices/Messages/GraphInfosRequest.h \
    Network/GenericServices/Messages/GraphInfosResponce.h \
    Network/GenericServices/Messages/HeaterParamsListResponse.h \
    Network/GenericServices/Messages/HeaterParamsRequest.h \
    Network/GenericServices/Messages/HeaterStateListResponse.h \
    Network/GenericServices/Messages/HeaterStateRequest.h \
    Network/GenericServices/Messages/HeaterStateResponse.h \
    Network/GenericServices/Messages/LightPressetsResponse.h \
    Network/GenericServices/Messages/LivestockOperationRequest.h \
    Network/GenericServices/Messages/LivestockStateResponse.h \
    Network/GenericServices/Messages/PreparingConfigRequest.h \
    Network/GenericServices/Messages/ProductionConfigRequest.h \
    Network/GenericServices/Messages/ProductionStateResponse.h \
    Network/GenericServices/Messages/RelayInfoListResponse.h \
    Network/GenericServices/Messages/RemoveGraphRequest.h \
    Network/GenericServices/Messages/SchedulerInfoResponse.h \
    Network/GenericServices/Messages/SensorsServiceRequest.h \
    Network/GenericServices/Messages/SensorsServiceResponse.h \
    Network/GenericServices/Messages/ServerInfoRequest.h \
    Network/GenericServices/Messages/ServoStateResponse.h \
    Network/GenericServices/Messages/SetProfileRequest.h \
    Network/GenericServices/Messages/TemperatureStateResponse.h \
    Network/GenericServices/Messages/UpdateMinMaxByDayProfileRequest.h \
    Network/GenericServices/Messages/UpdateServoStateRequest.h \
    Network/GenericServices/Messages/UpdateValueByDayProfileRequest.h \
    Network/GenericServices/Messages/UpdateValueByValueProfileRequest.h \
    Network/GenericServices/Messages/VentilationParamsRequest.h \
    Network/GenericServices/Messages/VentilationParamsResponse.h \
    Network/GenericServices/Messages/VentilationStatusResponse.h \
    Network/GenericServices/ProductionService.h \
    Network/GenericServices/SchedulerControlService.h \
    Network/GenericServices/SensorsService.h \
    Network/GenericServices/ServerInfoService.h \
    Network/GenericServices/SystemStatusService.h \
    Network/GenericServices/VentilationService.h \
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
    Controls/AnalogModeSwitch.ui \
    Controls/FanModeSwitch.ui \
    Controls/qclickablelineedit.ui \
    Frames/Dialogs/ConfigValveDialog.ui \
    Frames/Dialogs/EditAnalogFanDialog.ui \
    Frames/Dialogs/EditFanDialog.ui \
    Frames/Dialogs/HeaterConfigDialog.ui \
    Frames/Dialogs/LivestockOperationDialog.ui \
    Frames/Dialogs/MessageDialog.ui \
    Frames/Dialogs/MinMaxByDayEditDialog.ui \
    Frames/Dialogs/StartPreparingDialog.ui \
    Frames/Dialogs/StartProductionDialog.ui \
    Frames/Dialogs/ValueByDayEditDialog.ui \
    Frames/Dialogs/ValueByValueEditDialog.ui \
    Frames/Dialogs/inputdigitdialog.ui \
    Frames/Dialogs/inputtextdialog.ui \
    Frames/Graphs/ConfigProfilesFrame.ui \
    Frames/Graphs/SelectProfileFrame.ui \
    Frames/Graphs/TempProfileEditorFrame.ui \
    Frames/Graphs/ValveProfileEditorFrame.ui \
    Frames/Graphs/VentProfileEditorFrame.ui \
    Frames/HeaterControllerFrame.ui \
    Frames/Hystory/TemperatureHystoryFrame.ui \
    Frames/HystoryMenuFrame.ui \
    Frames/LightConfigFrame.ui \
    Frames/MainMenuFrame.ui \
    Frames/ProductionFrame.ui \
    Frames/SelectLightPresetFrame.ui \
    Frames/SystemStateFrame.ui \
    Frames/TemperatureConfigFrame.ui \
    Frames/TemperatureOwerviewFrame.ui \
    Frames/TestModeFrame.ui \
    Frames/VentControllerConfigFrame.ui \
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
    Images/save.png \
    mysql/README \
    mysql/mysql.json \
    mysql/mysql.pro.user \
    mysql/mysql.pro.user.aa93cc6

RESOURCES += \
    Resources.qrc

SUBDIRS += \
    mysql/mysql.pro
