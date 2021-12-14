#pragma once

#include <QDialog>

namespace Ui {
    class EditLightProfileDialog;
}

class EditLightProfileDialog : public QDialog
{
    Q_OBJECT
    Q_PROPERTY(QString Name READ Name WRITE setName NOTIFY NameChanged)
    Q_PROPERTY(QString Description READ Description WRITE setDescription NOTIFY DescriptionChanged)
public:
    explicit EditLightProfileDialog(QWidget *parent = nullptr);
    ~EditLightProfileDialog();

    const QString &Name() const;
    void setName(const QString &newName);

    const QString &Description() const;
    void setDescription(const QString &newDescription);
    void setTitle(const QString &title);
signals:
    void NameChanged();

    void DescriptionChanged();

private slots:
    void on_btnAccept_clicked();

    void on_btnReject_clicked();

private:
    Ui::EditLightProfileDialog *ui;
    QString m_Name;
    QString m_Description;
};

