using System.Collections.Generic;
using System;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Models.Widgets;
using System.Linq;

namespace WindowsFormsApp1.Services
{
    public class FormService
    {
        public void OpenForm(Widget widget, Line line)
        {
            if (line.WidgetType == WidgetTypeEnum.Animation)
            {
                var form = new AnimationForm(widget, line);
                form.ShowDialog();
            }
            else if (line.WidgetType == WidgetTypeEnum.Caption)
            {
                var form = new CaptionForm(widget, line);
                form.ShowDialog();
            }
            else if (line.WidgetType == WidgetTypeEnum.Text)
            {
                //TODO: Open text form
                var form = new QuizForm(widget, line);
                form.ShowDialog();
            }
            else if (line.WidgetType == WidgetTypeEnum.Selection)
            {
                var form = new SelectionForm(widget, line);
                form.ShowDialog();
            }
        }
        public void OpenForm(Widget widget)
        {
            if (widget.Lines.Any(c => c.WidgetType == WidgetTypeEnum.Caption))
            {
                var form = new CaptionForm(widget, widget.Lines.FirstOrDefault(c => c.WidgetType == WidgetTypeEnum.Caption));
                form.ShowDialog();
            }

        }
    }
}
