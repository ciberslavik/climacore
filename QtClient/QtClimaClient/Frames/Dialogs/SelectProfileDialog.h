#pragma once

#include <QDialog>
#include <QItemSelectionModel>
#include <QList>

#include <Models/Graphs/ProfileInfo.h>

#include <Models/Dialogs/ProfileInfoModel.h>

namespace Ui {
class SelectProfileDialog;
}

class SelectProfileDialog : public QDialog
{
    Q_OBJECT

public:
    explicit SelectProfileDialog(QList<ProfileInfo> *infos, QWidget *parent = nullptr);
    ~SelectProfileDialog();

    const QString& SelectedProfile();
    void setSelectedProfile(const QString &profileKey);
private slots:
    void on_btnUp_clicked();

    void on_btnDown_clicked();

    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

private:
    Ui::SelectProfileDialog *ui;
    QList<ProfileInfo> *m_profiles;
    ProfileInfo *m_selected;
    ProfileInfoModel *m_model;

    QItemSelectionModel *m_selection;

    void selectRow(int row);
};

