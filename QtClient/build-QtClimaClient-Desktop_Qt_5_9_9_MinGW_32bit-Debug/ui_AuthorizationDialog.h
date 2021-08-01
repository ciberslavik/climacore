/********************************************************************************
** Form generated from reading UI file 'AuthorizationDialog.ui'
**
** Created by: Qt User Interface Compiler version 5.9.9
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_AUTHORIZATIONDIALOG_H
#define UI_AUTHORIZATIONDIALOG_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QComboBox>
#include <QtWidgets/QDialog>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QSpacerItem>

QT_BEGIN_NAMESPACE

class Ui_AuthorizationDialog
{
public:
    QGridLayout *gridLayout;
    QLabel *label_2;
    QLineEdit *txtPassword;
    QComboBox *comboBox;
    QHBoxLayout *horizontalLayout;
    QPushButton *btnCancel;
    QPushButton *btnAccept;
    QLabel *label_3;
    QLabel *label;
    QSpacerItem *verticalSpacer;

    void setupUi(QDialog *AuthorizationDialog)
    {
        if (AuthorizationDialog->objectName().isEmpty())
            AuthorizationDialog->setObjectName(QStringLiteral("AuthorizationDialog"));
        AuthorizationDialog->resize(400, 210);
        gridLayout = new QGridLayout(AuthorizationDialog);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        gridLayout->setContentsMargins(25, -1, 25, -1);
        label_2 = new QLabel(AuthorizationDialog);
        label_2->setObjectName(QStringLiteral("label_2"));
        QFont font;
        font.setPointSize(14);
        label_2->setFont(font);

        gridLayout->addWidget(label_2, 1, 0, 1, 2);

        txtPassword = new QLineEdit(AuthorizationDialog);
        txtPassword->setObjectName(QStringLiteral("txtPassword"));
        txtPassword->setMinimumSize(QSize(0, 40));
        QFont font1;
        font1.setPointSize(16);
        txtPassword->setFont(font1);

        gridLayout->addWidget(txtPassword, 2, 2, 1, 1);

        comboBox = new QComboBox(AuthorizationDialog);
        comboBox->setObjectName(QStringLiteral("comboBox"));
        comboBox->setMinimumSize(QSize(0, 40));
        comboBox->setFont(font1);

        gridLayout->addWidget(comboBox, 1, 2, 1, 1);

        horizontalLayout = new QHBoxLayout();
        horizontalLayout->setObjectName(QStringLiteral("horizontalLayout"));
        horizontalLayout->setSizeConstraint(QLayout::SetMinimumSize);
        btnCancel = new QPushButton(AuthorizationDialog);
        btnCancel->setObjectName(QStringLiteral("btnCancel"));
        btnCancel->setMinimumSize(QSize(0, 45));

        horizontalLayout->addWidget(btnCancel);

        btnAccept = new QPushButton(AuthorizationDialog);
        btnAccept->setObjectName(QStringLiteral("btnAccept"));
        btnAccept->setMinimumSize(QSize(150, 45));

        horizontalLayout->addWidget(btnAccept);


        gridLayout->addLayout(horizontalLayout, 4, 0, 1, 3);

        label_3 = new QLabel(AuthorizationDialog);
        label_3->setObjectName(QStringLiteral("label_3"));
        label_3->setFont(font);

        gridLayout->addWidget(label_3, 2, 0, 1, 1);

        label = new QLabel(AuthorizationDialog);
        label->setObjectName(QStringLiteral("label"));
        label->setMinimumSize(QSize(0, 35));
        label->setMaximumSize(QSize(16777215, 36));
        QFont font2;
        font2.setPointSize(20);
        label->setFont(font2);
        label->setAlignment(Qt::AlignCenter);

        gridLayout->addWidget(label, 0, 0, 1, 3);

        verticalSpacer = new QSpacerItem(20, 40, QSizePolicy::Minimum, QSizePolicy::Expanding);

        gridLayout->addItem(verticalSpacer, 3, 2, 1, 1);


        retranslateUi(AuthorizationDialog);

        QMetaObject::connectSlotsByName(AuthorizationDialog);
    } // setupUi

    void retranslateUi(QDialog *AuthorizationDialog)
    {
        AuthorizationDialog->setWindowTitle(QApplication::translate("AuthorizationDialog", "Dialog", Q_NULLPTR));
        label_2->setText(QApplication::translate("AuthorizationDialog", "\320\237\320\276\320\273\321\214\320\267\320\276\320\262\320\260\321\202\320\265\320\273\321\214", Q_NULLPTR));
        txtPassword->setText(QApplication::translate("AuthorizationDialog", "abcd", Q_NULLPTR));
        comboBox->setCurrentText(QString());
        btnCancel->setText(QApplication::translate("AuthorizationDialog", "\320\236\321\202\320\274\320\265\320\275\320\260", Q_NULLPTR));
        btnAccept->setText(QApplication::translate("AuthorizationDialog", "OK", Q_NULLPTR));
        label_3->setText(QApplication::translate("AuthorizationDialog", "\320\237\320\260\321\200\320\276\320\273\321\214", Q_NULLPTR));
        label->setText(QApplication::translate("AuthorizationDialog", "\320\220\320\262\321\202\320\276\321\200\320\270\320\267\320\260\321\206\320\270\321\217", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class AuthorizationDialog: public Ui_AuthorizationDialog {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_AUTHORIZATIONDIALOG_H
