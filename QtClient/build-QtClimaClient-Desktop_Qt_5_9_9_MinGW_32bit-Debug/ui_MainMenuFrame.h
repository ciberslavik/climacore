/********************************************************************************
** Form generated from reading UI file 'MainMenuFrame.ui'
**
** Created by: Qt User Interface Compiler version 5.9.9
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINMENUFRAME_H
#define UI_MAINMENUFRAME_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QSpacerItem>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainMenuFrame
{
public:
    QGridLayout *gridLayout;
    QPushButton *btnReturn;
    QPushButton *btnLightConfig;
    QPushButton *btnVentilationOverview;
    QPushButton *btnEditProfiles;
    QPushButton *btnProduction;
    QPushButton *btnShowHystoryMenu;
    QPushButton *btnTemperature;
    QSpacerItem *verticalSpacer;
    QPushButton *btnShowDebugFrame;

    void setupUi(QWidget *MainMenuFrame)
    {
        if (MainMenuFrame->objectName().isEmpty())
            MainMenuFrame->setObjectName(QStringLiteral("MainMenuFrame"));
        MainMenuFrame->resize(674, 424);
        QFont font;
        font.setPointSize(14);
        MainMenuFrame->setFont(font);
        gridLayout = new QGridLayout(MainMenuFrame);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        gridLayout->setHorizontalSpacing(10);
        gridLayout->setVerticalSpacing(25);
        btnReturn = new QPushButton(MainMenuFrame);
        btnReturn->setObjectName(QStringLiteral("btnReturn"));
        btnReturn->setMinimumSize(QSize(0, 65));
        QIcon icon;
        icon.addFile(QStringLiteral(":/Images/icons8-u-turn-to-left-50.png"), QSize(), QIcon::Normal, QIcon::Off);
        btnReturn->setIcon(icon);
        btnReturn->setIconSize(QSize(42, 42));

        gridLayout->addWidget(btnReturn, 4, 0, 1, 1);

        btnLightConfig = new QPushButton(MainMenuFrame);
        btnLightConfig->setObjectName(QStringLiteral("btnLightConfig"));
        btnLightConfig->setMinimumSize(QSize(0, 65));
        QIcon icon1;
        icon1.addFile(QStringLiteral(":/Images/lamp.png"), QSize(), QIcon::Normal, QIcon::Off);
        btnLightConfig->setIcon(icon1);
        btnLightConfig->setIconSize(QSize(42, 42));

        gridLayout->addWidget(btnLightConfig, 0, 1, 1, 1);

        btnVentilationOverview = new QPushButton(MainMenuFrame);
        btnVentilationOverview->setObjectName(QStringLiteral("btnVentilationOverview"));
        btnVentilationOverview->setMinimumSize(QSize(0, 65));
        QIcon icon2;
        icon2.addFile(QStringLiteral(":/Images/ventilator.png"), QSize(), QIcon::Normal, QIcon::Off);
        btnVentilationOverview->setIcon(icon2);
        btnVentilationOverview->setIconSize(QSize(42, 42));

        gridLayout->addWidget(btnVentilationOverview, 1, 0, 1, 1);

        btnEditProfiles = new QPushButton(MainMenuFrame);
        btnEditProfiles->setObjectName(QStringLiteral("btnEditProfiles"));
        btnEditProfiles->setMinimumSize(QSize(0, 65));
        btnEditProfiles->setMaximumSize(QSize(16777215, 65));
        QIcon icon3;
        icon3.addFile(QStringLiteral(":/Images/line-chart.png"), QSize(), QIcon::Normal, QIcon::Off);
        btnEditProfiles->setIcon(icon3);
        btnEditProfiles->setIconSize(QSize(42, 42));

        gridLayout->addWidget(btnEditProfiles, 2, 0, 1, 1);

        btnProduction = new QPushButton(MainMenuFrame);
        btnProduction->setObjectName(QStringLiteral("btnProduction"));
        btnProduction->setMinimumSize(QSize(0, 65));
        QIcon icon4;
        icon4.addFile(QStringLiteral(":/Images/production.png"), QSize(), QIcon::Normal, QIcon::Off);
        btnProduction->setIcon(icon4);
        btnProduction->setIconSize(QSize(42, 42));

        gridLayout->addWidget(btnProduction, 1, 1, 1, 1);

        btnShowHystoryMenu = new QPushButton(MainMenuFrame);
        btnShowHystoryMenu->setObjectName(QStringLiteral("btnShowHystoryMenu"));
        btnShowHystoryMenu->setMinimumSize(QSize(0, 65));
        btnShowHystoryMenu->setMaximumSize(QSize(16777215, 65));
        QIcon icon5;
        icon5.addFile(QStringLiteral(":/Images/hystory.png"), QSize(), QIcon::Normal, QIcon::Off);
        btnShowHystoryMenu->setIcon(icon5);
        btnShowHystoryMenu->setIconSize(QSize(42, 42));

        gridLayout->addWidget(btnShowHystoryMenu, 2, 1, 1, 1);

        btnTemperature = new QPushButton(MainMenuFrame);
        btnTemperature->setObjectName(QStringLiteral("btnTemperature"));
        btnTemperature->setMinimumSize(QSize(0, 65));
        QIcon icon6;
        icon6.addFile(QStringLiteral(":/Images/temperature.png"), QSize(), QIcon::Normal, QIcon::Off);
        btnTemperature->setIcon(icon6);
        btnTemperature->setIconSize(QSize(42, 42));

        gridLayout->addWidget(btnTemperature, 0, 0, 1, 1);

        verticalSpacer = new QSpacerItem(20, 40, QSizePolicy::Minimum, QSizePolicy::Expanding);

        gridLayout->addItem(verticalSpacer, 3, 0, 1, 1);

        btnShowDebugFrame = new QPushButton(MainMenuFrame);
        btnShowDebugFrame->setObjectName(QStringLiteral("btnShowDebugFrame"));
        btnShowDebugFrame->setMinimumSize(QSize(0, 65));
        btnShowDebugFrame->setMaximumSize(QSize(16777215, 65));
        QIcon icon7;
        icon7.addFile(QStringLiteral(":/Images/enginer.png"), QSize(), QIcon::Normal, QIcon::Off);
        btnShowDebugFrame->setIcon(icon7);
        btnShowDebugFrame->setIconSize(QSize(42, 42));

        gridLayout->addWidget(btnShowDebugFrame, 3, 1, 1, 1);


        retranslateUi(MainMenuFrame);

        QMetaObject::connectSlotsByName(MainMenuFrame);
    } // setupUi

    void retranslateUi(QWidget *MainMenuFrame)
    {
        MainMenuFrame->setWindowTitle(QApplication::translate("MainMenuFrame", "Form", Q_NULLPTR));
        btnReturn->setText(QApplication::translate("MainMenuFrame", "\320\235\320\260\320\267\320\260\320\264", Q_NULLPTR));
        btnLightConfig->setText(QApplication::translate("MainMenuFrame", "\320\236\321\201\320\262\320\265\321\211\320\265\320\275\320\270\320\265", Q_NULLPTR));
        btnVentilationOverview->setText(QApplication::translate("MainMenuFrame", "\320\236\320\261\320\267\320\276\321\200 \320\262\320\265\320\275\321\202\320\270\320\273\321\217\321\206\320\270\320\270", Q_NULLPTR));
        btnEditProfiles->setText(QApplication::translate("MainMenuFrame", "\320\223\321\200\320\260\321\204\320\270\320\272\320\270", Q_NULLPTR));
        btnProduction->setText(QApplication::translate("MainMenuFrame", "\320\237\321\200\320\276\320\270\320\267\320\262\320\276\320\264\321\201\321\202\320\262\320\276", Q_NULLPTR));
        btnShowHystoryMenu->setText(QApplication::translate("MainMenuFrame", "\320\230\321\201\321\202\320\276\321\200\320\270\321\217", Q_NULLPTR));
        btnTemperature->setText(QApplication::translate("MainMenuFrame", "\320\242\320\265\320\274\320\277\320\265\321\200\320\260\321\202\321\203\321\200\320\260", Q_NULLPTR));
        btnShowDebugFrame->setText(QApplication::translate("MainMenuFrame", "\320\236\321\202\320\273\320\260\320\264\320\272\320\260", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class MainMenuFrame: public Ui_MainMenuFrame {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINMENUFRAME_H
