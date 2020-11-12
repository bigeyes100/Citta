﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using C2.Core;
using C2.Core.Documents;
using C2.Model;
using C2.Model.Documents;
using C2.Model.MindMaps;
using C2.Model.Widgets;

namespace C2.Controls.MapViews
{
    class PasteCommand : Command
    {
        Topic Topic;
        ChartObject[] PasteObjects;

        public PasteCommand(Document document, Topic topic)
        {
            Document = document;
            Topic = topic;

            if (Topic == null)
                throw new ArgumentNullException();
        }

        public override string Name
        {
            get { return "Paste"; }
        }

        public Document Document { get; private set; }

        public override bool Rollback()
        {
            AfterSelection = null;

            if (PasteObjects != null)
            {
                foreach (var obj in PasteObjects)
                {
                    //撤回的时候，原先是直接判断为topic，存在bug
                    if(obj is Topic)
                    {
                        Topic topic = obj as Topic;
                        if (topic.ParentTopic != null)
                        {
                            topic.ParentTopic.Children.Remove(topic);
                        }
                    }
                    else if(obj is Widget)
                    {
                        Widget widget = obj as Widget;
                        (widget.Container as Topic).Remove(widget);
                    }

                }

                PasteObjects = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Execute()
        {
            AfterSelection = null;

            if (Clipboard.ContainsData(typeof(MapClipboardData).ToString()))
            {
                return PasteTopic(Topic, Clipboard.GetData(typeof(MapClipboardData).ToString()));
            }
            else if (Clipboard.ContainsText())
            {
                return PasteText(Topic, Clipboard.GetText());
            }
            else
            {
                return false;
            }
        }

        bool PasteText(Topic topic, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var ts = new TextSerializer();
                var topics = ts.DeserializeTopic(text);
                if (topics != null && topics.Length > 0)
                {
                    foreach (Topic ct in topics)
                    {
                        topic.Children.Add(ct);
                    }
                    PasteObjects = topics;
                    AfterSelection = topics;
                }
            }

            return true;
        }

        bool PasteTopic(Topic topic, object data)
        {
            if (data is MapClipboardData)
            {
                var tcd = (MapClipboardData)data;
                //var topics = tcd.GetTopics();
                var chartObjects = PasteObjectsTo(tcd, Document, topic);
                if (chartObjects.IsNullOrEmpty())
                    return true;

                PasteObjects = chartObjects;
                AfterSelection = chartObjects;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static ChartObject[] PasteObjectsTo(MapClipboardData data, Document document, Topic target)
        {
            var chartObjects = data.GetChartObjects();
            if (!chartObjects.IsNullOrEmpty())
            {
                var newids = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var co in chartObjects)
                {
                    if (!string.IsNullOrEmpty(co.ID))
                    {
                        string id = co.ID;
                        co.ID = document.GetNextObjectID();
                        newids[id] = co.ID;
                    }
                }

                foreach (var co in chartObjects)
                {
                    if (co is Topic)
                    {
                        var t = (Topic)co;
                        target.Children.Add(t);

                        for (int j = t.Links.Count - 1; j >= 0; j--)
                        {
                            Link line = t.Links[j];
                            if (newids.ContainsKey(line.TargetID))
                                line.TargetID = (string)newids[line.TargetID];
                            else
                                t.Links.Remove(line);
                        }
                    }
                    else if (co is NoteWidget || co is PictureWidget || co is ProgressBarWidget)
                    {
                        target.Widgets.Add((Widget)co);
                    }
                }
            }

            return chartObjects;
        }
    }
}
