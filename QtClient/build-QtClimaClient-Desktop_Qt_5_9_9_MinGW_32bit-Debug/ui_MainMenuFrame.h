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
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainMenuFrame
{
public:
    QGridLayout *gridLayout;
    QPushButton *pushButton;
    QPushButton *pushButton_2;
    QPushButton *pushButton_3;
    QPushButton *pushButton_4;
    QPushButton *pushButton_5;
    QPushButton *pushButton_6;

    void setupUi(QWidget *MainMenuFrame)
    {
        if (MainMenuFrame->objectName().isEmpty())
            MainMenuFrame->setObjectName(QStringLiteral("MainMenuFrame"));
        MainMenuFrame->resize(674, 424);
        gridLayout = new QGridLayout(MainMenuFrame);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        pushButton = new QPushButton(MainMenuFrame);
        pushButton->setObjectName(QStringLiteral("pushButton"));
        pushButton->setMinimumSize(QSize(0, 65));

        gridLayout->addWidget(pushButton, 0, 0, 1, 1);

        pushButton_2 = new QPushButton(MainMenuFrame);
        pushButton_2->setObjectName(QStringLiteral("pushButton_2"));
        pushButton_2->setMinimumSize(QSize(0, 65));

        gridLayout->addWidget(pushButton_2, 0, 1, 1, 1);

        pushButton_3 = new QPushButton(MainMenuFrame);
        pushButton_3->setObjectName(QStringLiteral("pushButton_3"));
        pushButton_3->setMinimumSize(QSize(0, 65));

        gridLayout->addWidget(pushButton_3, 1, 0, 1, 1);

        pushButton_4 = new QPushButton(MainMenuFrame);
        pushButton_4->setObjectName(QStringLiteral("pushButton_4"));
        pushButton_4->setMinimumSize(QSize(0, 65));

        gridLayout->addWidget(pushButton_4, 1, 1, 1, 1);

        pushButton_5 = new QPushButton(MainMenuFrame);
        pushButton_5->setObjectName(QStringLiteral("pushButton_5"));
        pushButton_5->setMinimumSize(QSize(0, 65));

        gridLayout->addWidget(pushButton_5, 2, 0, 1, 1);

        pushButton_6 = new QPushButton(MainMenuFrame);
        pushButton_6->setObjectName(QStringLiteral("pushButton_6"));
        pushButton_6->setMinimumSize(QSize(0, 65));

        gridLayout->addWidget(pushButton_6, 2, 1, 1, 1);


        retranslateUi(MainMenuFrame);

        QMetaObject::connectSlotsByName(MainMenuFrame);
    } // setupUi

    void retranslateUi(QWidget *MainMenuFrame)
    {
        MainMenuFrame->setWindowTitle(QApplication::translate("MainMenuFrame", "Form", Q_NULLPTR));
        pushButton->setText(QApplication::translate("MainMenuFrame", "PushButton", Q_NULLPTR));
        pushButton_2->setText(QApplication::translate("MainMenuFrame", "PushButton", Q_NULLPTR));
        pushButton_3->setText(QApplication::translate("MainMenuFrame", "PushButton", Q_NULLPTR));
        pushButton_4->setText(QApplication::translate("MainMenuFrame", "PushButton", Q_NULLPTR));
        pushButton_5->setText(QApplication::translate("MainMenuFrame", "PushButton", Q_NULLPTR));
        pushButton_6->setText(QApplication::translate("MainMenuFrame", "PushButton", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class MainMenuFrame: public Ui_MainMenuFrame {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINMENUFRAME_H
