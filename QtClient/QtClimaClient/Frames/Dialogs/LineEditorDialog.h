#pragma once

#include <QDialog>
#include <QValidator>

namespace Ui {
    class LineEditorDialog;
}

class LineEditorDialog : public QDialog
{
    Q_OBJECT

public:
    explicit LineEditorDialog(QWidget *parent = nullptr);
    ~LineEditorDialog();
    void setTitle(const QString &title);
    void setValueName(const QString &valueName);
    void setValue(const QString &value);
    void setValidator(QValidator *validator);
    QString value() const;
private slots:
    void on_btnAccept_clicked();

    void on_btnReject_clicked();

private:
    Ui::LineEditorDialog *ui;
};

