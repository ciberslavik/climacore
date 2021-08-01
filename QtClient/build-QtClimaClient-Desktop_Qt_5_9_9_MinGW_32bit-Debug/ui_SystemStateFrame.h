/********************************************************************************
** Form generated from reading UI file 'SystemStateFrame.ui'
**
** Created by: Qt User Interface Compiler version 5.9.9
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_SYSTEMSTATEFRAME_H
#define UI_SYSTEMSTATEFRAME_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QGroupBox>
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QProgressBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_SystemStateFrame
{
public:
    QVBoxLayout *verticalLayout;
    QGroupBox *groupBox_2;
    QGridLayout *gridLayout_2;
    QLabel *label_9;
    QLabel *label_7;
    QLabel *lblFrontTemp;
    QLabel *label_8;
    QLabel *lblRearTemp;
    QLabel *lblHumidity;
    QLabel *lblOutdoorTemp;
    QLabel *label_10;
    QLabel *label_2;
    QLabel *lblTempSetpoint;
    QPushButton *pushButton_3;
    QGroupBox *groupBox_3;
    QGridLayout *gridLayout_3;
    QLabel *label_11;
    QProgressBar *barAnalogFan;
    QLabel *label_14;
    QProgressBar *barMines;
    QLabel *label_13;
    QProgressBar *barValves;
    QProgressBar *progressBar_4;
    QLabel *label_5;
    QLabel *label_6;
    QLabel *lblAirPerHead;
    QLabel *label;
    QProgressBar *progressBar;
    QHBoxLayout *horizontalLayout;
    QPushButton *pushButton_2;
    QPushButton *pushButton;

    void setupUi(QWidget *SystemStateFrame)
    {
        if (SystemStateFrame->objectName().isEmpty())
            SystemStateFrame->setObjectName(QStringLiteral("SystemStateFrame"));
        SystemStateFrame->resize(669, 416);
        verticalLayout = new QVBoxLayout(SystemStateFrame);
        verticalLayout->setSpacing(3);
        verticalLayout->setObjectName(QStringLiteral("verticalLayout"));
        verticalLayout->setContentsMargins(0, 0, 0, 0);
        groupBox_2 = new QGroupBox(SystemStateFrame);
        groupBox_2->setObjectName(QStringLiteral("groupBox_2"));
        QFont font;
        font.setPointSize(14);
        groupBox_2->setFont(font);
        groupBox_2->setAlignment(Qt::AlignCenter);
        gridLayout_2 = new QGridLayout(groupBox_2);
        gridLayout_2->setObjectName(QStringLiteral("gridLayout_2"));
        label_9 = new QLabel(groupBox_2);
        label_9->setObjectName(QStringLiteral("label_9"));
        QFont font1;
        font1.setPointSize(15);
        label_9->setFont(font1);

        gridLayout_2->addWidget(label_9, 0, 3, 1, 1);

        label_7 = new QLabel(groupBox_2);
        label_7->setObjectName(QStringLiteral("label_7"));
        label_7->setFont(font1);

        gridLayout_2->addWidget(label_7, 0, 0, 1, 1);

        lblFrontTemp = new QLabel(groupBox_2);
        lblFrontTemp->setObjectName(QStringLiteral("lblFrontTemp"));
        QFont font2;
        font2.setPointSize(15);
        font2.setBold(true);
        font2.setWeight(75);
        lblFrontTemp->setFont(font2);

        gridLayout_2->addWidget(lblFrontTemp, 0, 2, 1, 1);

        label_8 = new QLabel(groupBox_2);
        label_8->setObjectName(QStringLiteral("label_8"));
        label_8->setFont(font1);

        gridLayout_2->addWidget(label_8, 1, 0, 1, 1);

        lblRearTemp = new QLabel(groupBox_2);
        lblRearTemp->setObjectName(QStringLiteral("lblRearTemp"));
        lblRearTemp->setFont(font2);

        gridLayout_2->addWidget(lblRearTemp, 0, 4, 1, 1);

        lblHumidity = new QLabel(groupBox_2);
        lblHumidity->setObjectName(QStringLiteral("lblHumidity"));
        lblHumidity->setFont(font2);

        gridLayout_2->addWidget(lblHumidity, 1, 4, 1, 1);

        lblOutdoorTemp = new QLabel(groupBox_2);
        lblOutdoorTemp->setObjectName(QStringLiteral("lblOutdoorTemp"));
        lblOutdoorTemp->setFont(font2);

        gridLayout_2->addWidget(lblOutdoorTemp, 1, 2, 1, 1);

        label_10 = new QLabel(groupBox_2);
        label_10->setObjectName(QStringLiteral("label_10"));
        label_10->setFont(font1);

        gridLayout_2->addWidget(label_10, 1, 3, 1, 1);

        label_2 = new QLabel(groupBox_2);
        label_2->setObjectName(QStringLiteral("label_2"));
        label_2->setFont(font1);

        gridLayout_2->addWidget(label_2, 2, 0, 1, 1);

        lblTempSetpoint = new QLabel(groupBox_2);
        lblTempSetpoint->setObjectName(QStringLiteral("lblTempSetpoint"));
        lblTempSetpoint->setFont(font2);

        gridLayout_2->addWidget(lblTempSetpoint, 2, 2, 1, 1);

        pushButton_3 = new QPushButton(groupBox_2);
        pushButton_3->setObjectName(QStringLiteral("pushButton_3"));

        gridLayout_2->addWidget(pushButton_3, 2, 3, 1, 1);


        verticalLayout->addWidget(groupBox_2);

        groupBox_3 = new QGroupBox(SystemStateFrame);
        groupBox_3->setObjectName(QStringLiteral("groupBox_3"));
        groupBox_3->setFont(font);
        groupBox_3->setAlignment(Qt::AlignCenter);
        gridLayout_3 = new QGridLayout(groupBox_3);
        gridLayout_3->setObjectName(QStringLiteral("gridLayout_3"));
        label_11 = new QLabel(groupBox_3);
        label_11->setObjectName(QStringLiteral("label_11"));

        gridLayout_3->addWidget(label_11, 0, 0, 1, 1);

        barAnalogFan = new QProgressBar(groupBox_3);
        barAnalogFan->setObjectName(QStringLiteral("barAnalogFan"));
        barAnalogFan->setValue(24);

        gridLayout_3->addWidget(barAnalogFan, 0, 1, 1, 4);

        label_14 = new QLabel(groupBox_3);
        label_14->setObjectName(QStringLiteral("label_14"));
        label_14->setMaximumSize(QSize(75, 16777215));

        gridLayout_3->addWidget(label_14, 4, 2, 1, 1);

        barMines = new QProgressBar(groupBox_3);
        barMines->setObjectName(QStringLiteral("barMines"));
        barMines->setValue(24);

        gridLayout_3->addWidget(barMines, 1, 1, 1, 4);

        label_13 = new QLabel(groupBox_3);
        label_13->setObjectName(QStringLiteral("label_13"));

        gridLayout_3->addWidget(label_13, 1, 0, 2, 1);

        barValves = new QProgressBar(groupBox_3);
        barValves->setObjectName(QStringLiteral("barValves"));
        barValves->setValue(24);

        gridLayout_3->addWidget(barValves, 3, 1, 1, 4);

        progressBar_4 = new QProgressBar(groupBox_3);
        progressBar_4->setObjectName(QStringLiteral("progressBar_4"));
        progressBar_4->setValue(24);

        gridLayout_3->addWidget(progressBar_4, 4, 3, 1, 2);

        label_5 = new QLabel(groupBox_3);
        label_5->setObjectName(QStringLiteral("label_5"));

        gridLayout_3->addWidget(label_5, 3, 0, 1, 1);

        label_6 = new QLabel(groupBox_3);
        label_6->setObjectName(QStringLiteral("label_6"));

        gridLayout_3->addWidget(label_6, 4, 0, 1, 1);

        lblAirPerHead = new QLabel(groupBox_3);
        lblAirPerHead->setObjectName(QStringLiteral("lblAirPerHead"));
        lblAirPerHead->setMinimumSize(QSize(50, 0));
        lblAirPerHead->setMaximumSize(QSize(60, 16777215));

        gridLayout_3->addWidget(lblAirPerHead, 4, 1, 1, 1);

        label = new QLabel(groupBox_3);
        label->setObjectName(QStringLiteral("label"));

        gridLayout_3->addWidget(label, 5, 0, 1, 1);

        progressBar = new QProgressBar(groupBox_3);
        progressBar->setObjectName(QStringLiteral("progressBar"));
        progressBar->setValue(24);

        gridLayout_3->addWidget(progressBar, 5, 1, 1, 4);


        verticalLayout->addWidget(groupBox_3);

        horizontalLayout = new QHBoxLayout();
        horizontalLayout->setObjectName(QStringLiteral("horizontalLayout"));
        pushButton_2 = new QPushButton(SystemStateFrame);
        pushButton_2->setObjectName(QStringLiteral("pushButton_2"));
        pushButton_2->setMinimumSize(QSize(0, 45));

        horizontalLayout->addWidget(pushButton_2);

        pushButton = new QPushButton(SystemStateFrame);
        pushButton->setObjectName(QStringLiteral("pushButton"));
        pushButton->setMinimumSize(QSize(0, 45));

        horizontalLayout->addWidget(pushButton);


        verticalLayout->addLayout(horizontalLayout);


        retranslateUi(SystemStateFrame);

        QMetaObject::connectSlotsByName(SystemStateFrame);
    } // setupUi

    void retranslateUi(QWidget *SystemStateFrame)
    {
        SystemStateFrame->setWindowTitle(QApplication::translate("SystemStateFrame", "Form", Q_NULLPTR));
        groupBox_2->setTitle(QApplication::translate("SystemStateFrame", "\320\242\320\265\320\274\320\277\320\265\321\200\320\260\321\202\321\203\321\200\320\260", Q_NULLPTR));
        label_9->setText(QApplication::translate("SystemStateFrame", "\320\242\321\213\320\273", Q_NULLPTR));
        label_7->setText(QApplication::translate("SystemStateFrame", "\320\244\321\200\320\276\320\275\321\202", Q_NULLPTR));
        lblFrontTemp->setText(QApplication::translate("SystemStateFrame", "10.5", Q_NULLPTR));
        label_8->setText(QApplication::translate("SystemStateFrame", "\320\243\320\273\320\270\321\206\320\260", Q_NULLPTR));
        lblRearTemp->setText(QApplication::translate("SystemStateFrame", "10.5", Q_NULLPTR));
        lblHumidity->setText(QApplication::translate("SystemStateFrame", "10.5", Q_NULLPTR));
        lblOutdoorTemp->setText(QApplication::translate("SystemStateFrame", "10.5", Q_NULLPTR));
        label_10->setText(QApplication::translate("SystemStateFrame", "\320\222\320\273\320\260\320\266\320\275\320\276\321\201\321\202\321\214", Q_NULLPTR));
        label_2->setText(QApplication::translate("SystemStateFrame", "\320\227\320\260\320\264\320\260\320\275\320\275\320\260\321\217", Q_NULLPTR));
        lblTempSetpoint->setText(QApplication::translate("SystemStateFrame", "10", Q_NULLPTR));
        pushButton_3->setText(QApplication::translate("SystemStateFrame", "PushButton", Q_NULLPTR));
        groupBox_3->setTitle(QApplication::translate("SystemStateFrame", "\320\222\320\265\320\275\321\202\320\270\320\273\321\217\321\206\320\270\321\217", Q_NULLPTR));
        label_11->setText(QApplication::translate("SystemStateFrame", "\320\243\320\277\321\200\320\260\320\262\320\273\321\217\320\265\320\274\321\213\320\271", Q_NULLPTR));
        label_14->setText(QApplication::translate("SystemStateFrame", "m<sup>3</sup>h/\320\263\320\276\320\273.", Q_NULLPTR));
        label_13->setText(QApplication::translate("SystemStateFrame", "\320\250\320\260\321\205\321\202\321\213", Q_NULLPTR));
        label_5->setText(QApplication::translate("SystemStateFrame", "\320\232\320\273\320\260\320\277\320\260\320\275\320\260", Q_NULLPTR));
        label_6->setText(QApplication::translate("SystemStateFrame", "TextLabel", Q_NULLPTR));
        lblAirPerHead->setText(QApplication::translate("SystemStateFrame", "TextLabel", Q_NULLPTR));
        label->setText(QApplication::translate("SystemStateFrame", "\320\240\320\260\320\267\321\200\320\265\320\266\320\265\320\275\320\270\320\265", Q_NULLPTR));
        progressBar->setFormat(QApplication::translate("SystemStateFrame", "%p Pa", Q_NULLPTR));
        pushButton_2->setText(QApplication::translate("SystemStateFrame", "\320\222\321\213\320\272\320\273 \321\215\320\272\321\200\320\260\320\275", Q_NULLPTR));
        pushButton->setText(QApplication::translate("SystemStateFrame", "\320\223\320\273\320\260\320\262\320\275\320\276\320\265 \320\274\320\265\320\275\321\216", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class SystemStateFrame: public Ui_SystemStateFrame {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_SYSTEMSTATEFRAME_H
