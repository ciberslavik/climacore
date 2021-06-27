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
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_SystemStateFrame
{
public:
    QVBoxLayout *verticalLayout;
    QGroupBox *groupBox;
    QGridLayout *gridLayout;
    QLabel *label_5;
    QLineEdit *txt_CurrentLivestock;
    QLabel *label;
    QLineEdit *lineEdit;
    QLabel *label_6;
    QLineEdit *lineEdit_6;
    QLabel *label_2;
    QLineEdit *lineEdit_3;
    QLabel *label_3;
    QLineEdit *lineEdit_2;
    QLabel *label_4;
    QLineEdit *lineEdit_4;
    QGroupBox *groupBox_2;
    QGridLayout *gridLayout_2;
    QLabel *label_7;
    QLineEdit *lineEdit_5;
    QLabel *label_9;
    QLineEdit *lineEdit_7;
    QLabel *label_8;
    QLineEdit *lineEdit_8;
    QLabel *label_10;
    QLineEdit *lineEdit_9;
    QGroupBox *groupBox_3;
    QGridLayout *gridLayout_3;
    QLabel *label_11;
    QLineEdit *lineEdit_10;
    QLabel *label_12;
    QLineEdit *lineEdit_11;
    QLabel *label_13;
    QLineEdit *lineEdit_12;
    QLabel *label_14;
    QLineEdit *lineEdit_13;
    QHBoxLayout *horizontalLayout;
    QPushButton *pushButton_2;
    QPushButton *pushButton;

    void setupUi(QWidget *SystemStateFrame)
    {
        if (SystemStateFrame->objectName().isEmpty())
            SystemStateFrame->setObjectName(QStringLiteral("SystemStateFrame"));
        SystemStateFrame->resize(653, 404);
        verticalLayout = new QVBoxLayout(SystemStateFrame);
        verticalLayout->setObjectName(QStringLiteral("verticalLayout"));
        groupBox = new QGroupBox(SystemStateFrame);
        groupBox->setObjectName(QStringLiteral("groupBox"));
        gridLayout = new QGridLayout(groupBox);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        label_5 = new QLabel(groupBox);
        label_5->setObjectName(QStringLiteral("label_5"));

        gridLayout->addWidget(label_5, 0, 0, 1, 1);

        txt_CurrentLivestock = new QLineEdit(groupBox);
        txt_CurrentLivestock->setObjectName(QStringLiteral("txt_CurrentLivestock"));
        txt_CurrentLivestock->setReadOnly(true);

        gridLayout->addWidget(txt_CurrentLivestock, 0, 1, 1, 1);

        label = new QLabel(groupBox);
        label->setObjectName(QStringLiteral("label"));

        gridLayout->addWidget(label, 0, 2, 1, 1);

        lineEdit = new QLineEdit(groupBox);
        lineEdit->setObjectName(QStringLiteral("lineEdit"));
        lineEdit->setReadOnly(true);

        gridLayout->addWidget(lineEdit, 0, 3, 1, 1);

        label_6 = new QLabel(groupBox);
        label_6->setObjectName(QStringLiteral("label_6"));

        gridLayout->addWidget(label_6, 1, 0, 1, 1);

        lineEdit_6 = new QLineEdit(groupBox);
        lineEdit_6->setObjectName(QStringLiteral("lineEdit_6"));
        lineEdit_6->setReadOnly(true);

        gridLayout->addWidget(lineEdit_6, 1, 1, 1, 1);

        label_2 = new QLabel(groupBox);
        label_2->setObjectName(QStringLiteral("label_2"));

        gridLayout->addWidget(label_2, 1, 2, 1, 1);

        lineEdit_3 = new QLineEdit(groupBox);
        lineEdit_3->setObjectName(QStringLiteral("lineEdit_3"));
        lineEdit_3->setReadOnly(true);

        gridLayout->addWidget(lineEdit_3, 1, 3, 1, 1);

        label_3 = new QLabel(groupBox);
        label_3->setObjectName(QStringLiteral("label_3"));

        gridLayout->addWidget(label_3, 2, 0, 1, 1);

        lineEdit_2 = new QLineEdit(groupBox);
        lineEdit_2->setObjectName(QStringLiteral("lineEdit_2"));
        lineEdit_2->setReadOnly(true);

        gridLayout->addWidget(lineEdit_2, 2, 1, 1, 1);

        label_4 = new QLabel(groupBox);
        label_4->setObjectName(QStringLiteral("label_4"));

        gridLayout->addWidget(label_4, 2, 2, 1, 1);

        lineEdit_4 = new QLineEdit(groupBox);
        lineEdit_4->setObjectName(QStringLiteral("lineEdit_4"));
        lineEdit_4->setReadOnly(true);

        gridLayout->addWidget(lineEdit_4, 2, 3, 1, 1);


        verticalLayout->addWidget(groupBox);

        groupBox_2 = new QGroupBox(SystemStateFrame);
        groupBox_2->setObjectName(QStringLiteral("groupBox_2"));
        gridLayout_2 = new QGridLayout(groupBox_2);
        gridLayout_2->setObjectName(QStringLiteral("gridLayout_2"));
        label_7 = new QLabel(groupBox_2);
        label_7->setObjectName(QStringLiteral("label_7"));

        gridLayout_2->addWidget(label_7, 0, 0, 1, 1);

        lineEdit_5 = new QLineEdit(groupBox_2);
        lineEdit_5->setObjectName(QStringLiteral("lineEdit_5"));
        lineEdit_5->setReadOnly(true);

        gridLayout_2->addWidget(lineEdit_5, 0, 1, 1, 1);

        label_9 = new QLabel(groupBox_2);
        label_9->setObjectName(QStringLiteral("label_9"));

        gridLayout_2->addWidget(label_9, 0, 2, 1, 1);

        lineEdit_7 = new QLineEdit(groupBox_2);
        lineEdit_7->setObjectName(QStringLiteral("lineEdit_7"));
        lineEdit_7->setReadOnly(true);

        gridLayout_2->addWidget(lineEdit_7, 0, 3, 1, 1);

        label_8 = new QLabel(groupBox_2);
        label_8->setObjectName(QStringLiteral("label_8"));

        gridLayout_2->addWidget(label_8, 1, 0, 1, 1);

        lineEdit_8 = new QLineEdit(groupBox_2);
        lineEdit_8->setObjectName(QStringLiteral("lineEdit_8"));
        lineEdit_8->setReadOnly(true);

        gridLayout_2->addWidget(lineEdit_8, 1, 1, 1, 1);

        label_10 = new QLabel(groupBox_2);
        label_10->setObjectName(QStringLiteral("label_10"));

        gridLayout_2->addWidget(label_10, 1, 2, 1, 1);

        lineEdit_9 = new QLineEdit(groupBox_2);
        lineEdit_9->setObjectName(QStringLiteral("lineEdit_9"));
        lineEdit_9->setReadOnly(true);

        gridLayout_2->addWidget(lineEdit_9, 1, 3, 1, 1);


        verticalLayout->addWidget(groupBox_2);

        groupBox_3 = new QGroupBox(SystemStateFrame);
        groupBox_3->setObjectName(QStringLiteral("groupBox_3"));
        gridLayout_3 = new QGridLayout(groupBox_3);
        gridLayout_3->setObjectName(QStringLiteral("gridLayout_3"));
        label_11 = new QLabel(groupBox_3);
        label_11->setObjectName(QStringLiteral("label_11"));

        gridLayout_3->addWidget(label_11, 0, 0, 1, 1);

        lineEdit_10 = new QLineEdit(groupBox_3);
        lineEdit_10->setObjectName(QStringLiteral("lineEdit_10"));
        lineEdit_10->setReadOnly(true);

        gridLayout_3->addWidget(lineEdit_10, 0, 1, 2, 1);

        label_12 = new QLabel(groupBox_3);
        label_12->setObjectName(QStringLiteral("label_12"));

        gridLayout_3->addWidget(label_12, 0, 2, 1, 1);

        lineEdit_11 = new QLineEdit(groupBox_3);
        lineEdit_11->setObjectName(QStringLiteral("lineEdit_11"));

        gridLayout_3->addWidget(lineEdit_11, 0, 3, 2, 1);

        label_13 = new QLabel(groupBox_3);
        label_13->setObjectName(QStringLiteral("label_13"));

        gridLayout_3->addWidget(label_13, 1, 0, 2, 1);

        lineEdit_12 = new QLineEdit(groupBox_3);
        lineEdit_12->setObjectName(QStringLiteral("lineEdit_12"));

        gridLayout_3->addWidget(lineEdit_12, 2, 1, 1, 1);

        label_14 = new QLabel(groupBox_3);
        label_14->setObjectName(QStringLiteral("label_14"));

        gridLayout_3->addWidget(label_14, 2, 2, 1, 1);

        lineEdit_13 = new QLineEdit(groupBox_3);
        lineEdit_13->setObjectName(QStringLiteral("lineEdit_13"));

        gridLayout_3->addWidget(lineEdit_13, 2, 3, 1, 1);


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
        groupBox->setTitle(QApplication::translate("SystemStateFrame", "\320\237\320\276\320\263\320\276\320\273\320\276\320\262\321\214\320\265", Q_NULLPTR));
        label_5->setText(QApplication::translate("SystemStateFrame", "\320\242\320\265\320\272\321\203\321\211\320\265\320\265 \320\277\320\276\320\263\320\276\320\273\320\276\320\262\321\214\320\265", Q_NULLPTR));
        label->setText(QApplication::translate("SystemStateFrame", "\320\227\320\260\321\201\320\265\320\273\320\265\320\275\320\276 \321\201\320\265\320\263\320\276\320\264\320\275\321\217", Q_NULLPTR));
        label_6->setText(QApplication::translate("SystemStateFrame", "\320\222\321\201\320\265\320\263\320\276 \320\267\320\260\321\201\320\265\320\273\320\265\320\275\320\276", Q_NULLPTR));
        label_2->setText(QApplication::translate("SystemStateFrame", "\320\222\321\201\320\265\320\263\320\276 \320\262\321\213\321\201\320\265\320\273\320\265\320\275\320\276", Q_NULLPTR));
        label_3->setText(QApplication::translate("SystemStateFrame", "\320\222\321\201\320\265\320\263\320\276 \320\277\320\260\320\264\320\265\320\266\321\214", Q_NULLPTR));
        label_4->setText(QApplication::translate("SystemStateFrame", "\320\237\320\260\320\264\320\265\320\266\321\214 \321\201\320\265\320\263\320\276\320\264\320\275\321\217", Q_NULLPTR));
        groupBox_2->setTitle(QApplication::translate("SystemStateFrame", "\320\242\320\265\320\274\320\277\320\265\321\200\320\260\321\202\321\203\321\200\320\260", Q_NULLPTR));
        label_7->setText(QApplication::translate("SystemStateFrame", "\320\242\321\213\320\273", Q_NULLPTR));
        label_9->setText(QApplication::translate("SystemStateFrame", "\320\244\321\200\320\276\320\275\321\202", Q_NULLPTR));
        label_8->setText(QApplication::translate("SystemStateFrame", "\320\243\320\273\320\270\321\206\320\260", Q_NULLPTR));
        label_10->setText(QApplication::translate("SystemStateFrame", "\320\222\320\273\320\260\320\266\320\275\320\276\321\201\321\202\321\214", Q_NULLPTR));
        groupBox_3->setTitle(QApplication::translate("SystemStateFrame", "\320\222\320\265\320\275\321\202\320\270\320\273\321\217\321\206\320\270\321\217", Q_NULLPTR));
        label_11->setText(QApplication::translate("SystemStateFrame", "\320\243\320\277\321\200\320\260\320\262\320\273\321\217\320\265\320\274\321\213\320\271 1", Q_NULLPTR));
        label_12->setText(QApplication::translate("SystemStateFrame", "\320\243\320\277\321\200\320\260\320\262\320\273\321\217\320\265\320\274\321\213\320\271 2", Q_NULLPTR));
        label_13->setText(QApplication::translate("SystemStateFrame", "\320\242\320\265\320\272\321\203\321\211\320\260\321\217 \320\274\320\276\321\211\320\275\320\276\321\201\321\202\321\214", Q_NULLPTR));
        label_14->setText(QApplication::translate("SystemStateFrame", "\320\224\320\270\321\201\320\272\321\200\320\265\321\202\320\275\321\213\320\265 \320\262\320\265\320\275\321\202.", Q_NULLPTR));
        pushButton_2->setText(QApplication::translate("SystemStateFrame", "\320\222\321\213\320\272\320\273 \321\215\320\272\321\200\320\260\320\275", Q_NULLPTR));
        pushButton->setText(QApplication::translate("SystemStateFrame", "\320\223\320\273\320\260\320\262\320\275\320\276\320\265 \320\274\320\265\320\275\321\216", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class SystemStateFrame: public Ui_SystemStateFrame {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_SYSTEMSTATEFRAME_H
