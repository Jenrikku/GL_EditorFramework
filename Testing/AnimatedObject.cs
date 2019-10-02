﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GL_EditorFramework;
using GL_EditorFramework.EditorDrawables;
using GL_EditorFramework.GL_Core;
using GL_EditorFramework.Interfaces;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Example
{
    //This class is supposed to show of some very basic animation stuff you could do with this framework
    //but it's highly recommended to add members like startTime and isPlaying if you want to make your own animated object class
    class AnimatedObject : SingleObject
    {
        public AnimatedObject(Vector3 pos) : base(pos)
        {

        }

        public override string ToString() => "moving platforms";

        static new Vector4 Color = new Vector4(1f, 0f, 0f, 1f);

        public override void Draw(GL_ControlModern control, Pass pass, EditorSceneBase editorScene)
        {
            if (pass == Pass.TRANSPARENT)
                return;

            if (!editorScene.ShouldBeDrawn(this))
                return;

            bool hovered = editorScene.Hovered == this;

            Matrix4 mtx = Matrix4.CreateScale(1f, 0.25f, 1f);
            mtx *= Matrix4.CreateFromAxisAngle(Vector3.UnitY, -Framework.HALF_PI);
            mtx *= Matrix4.CreateTranslation(Selected ? editorScene.CurrentAction.NewPos(Position) : Position);

            Vector4 pickingColor = control.NextPickingColor();
            
            Vector4 lineBoxColor;

            if (hovered && Selected)
                lineBoxColor = hoverColor;
            else if (hovered || Selected)
                lineBoxColor = selectColor;
            else
                lineBoxColor = new Vector4(1, 1, 1, 1);

            control.UpdateModelMatrix(mtx * Matrix4.CreateTranslation(Vector3.UnitX * (3f - Math.Abs(control.RedrawerFrame * 0.0625f % 6f - 3f))));

            Renderers.ColorBlockRenderer.Draw(control, pass, Color, Color, pickingColor);

            control.UpdateModelMatrix(mtx);

            Renderers.ColorBlockRenderer.DrawLineBox(control, pass, lineBoxColor, pickingColor);

            control.UpdateModelMatrix(mtx * Matrix4.CreateTranslation(Vector3.UnitX * 3f));

            Renderers.ColorBlockRenderer.DrawLineBox(control, pass, lineBoxColor, pickingColor);
        }

        public override void Draw(GL_ControlModern control, Pass pass)
        {
            if (pass == Pass.TRANSPARENT)
                return;

            Matrix4 mtx = Matrix4.CreateScale(1f, 0.25f, 1f);
            mtx *= Matrix4.CreateFromAxisAngle(Vector3.UnitY, -Framework.HALF_PI);
            mtx *= Matrix4.CreateTranslation(Position);
            control.UpdateModelMatrix(mtx);

            Vector4 pickingColor = control.NextPickingColor();

            Vector4 lineBoxColor = new Vector4(1, 1, 1, 1);

            control.UpdateModelMatrix(mtx * Matrix4.CreateTranslation(Vector3.UnitX * (3f - Math.Abs(control.RedrawerFrame * 0.0625f % 6f - 3f))));

            Renderers.ColorBlockRenderer.Draw(control, pass, Color, Color, pickingColor);

            control.UpdateModelMatrix(mtx);

            Renderers.ColorBlockRenderer.DrawLineBox(control, pass, lineBoxColor, pickingColor);

            control.UpdateModelMatrix(mtx * Matrix4.CreateTranslation(Vector3.UnitX * 3f));

            Renderers.ColorBlockRenderer.DrawLineBox(control, pass, lineBoxColor, pickingColor);
        }

        public override void Draw(GL_ControlLegacy control, Pass pass, EditorSceneBase editorScene)
        {
            if (pass == Pass.TRANSPARENT)
                return;

            if (!editorScene.ShouldBeDrawn(this))
                return;

            bool hovered = editorScene.Hovered == this;

            Matrix4 mtx = Matrix4.CreateScale(1f, 0.25f, 1f);
            mtx *= Matrix4.CreateFromAxisAngle(Vector3.UnitY, -Framework.HALF_PI);
            mtx *= Matrix4.CreateTranslation(Selected ? editorScene.CurrentAction.NewPos(Position) : Position);

            Vector4 pickingColor = control.NextPickingColor();

            Vector4 lineBoxColor;

            if (hovered && Selected)
                lineBoxColor = hoverColor;
            else if (hovered || Selected)
                lineBoxColor = selectColor;
            else
                lineBoxColor = new Vector4(1, 1, 1, 1);

            control.UpdateModelMatrix(mtx * Matrix4.CreateTranslation(Vector3.UnitX * (3f - Math.Abs(control.RedrawerFrame * 0.0625f % 6f - 3f))));

            Renderers.ColorBlockRenderer.Draw(control, pass, Color, Color, pickingColor);

            control.UpdateModelMatrix(mtx);

            Renderers.ColorBlockRenderer.DrawLineBox(control, pass, lineBoxColor, pickingColor);

            control.UpdateModelMatrix(mtx * Matrix4.CreateTranslation(Vector3.UnitX * 3f));

            Renderers.ColorBlockRenderer.DrawLineBox(control, pass, lineBoxColor, pickingColor);
        }

        public override void Draw(GL_ControlLegacy control, Pass pass)
        {
            if (pass == Pass.TRANSPARENT)
                return;

            Matrix4 mtx = Matrix4.CreateScale(1f, 0.25f, 1f);
            mtx *= Matrix4.CreateFromAxisAngle(Vector3.UnitY, -Framework.HALF_PI);
            mtx *= Matrix4.CreateTranslation(Position);
            control.UpdateModelMatrix(mtx);

            Vector4 pickingColor = control.NextPickingColor();

            Vector4 lineBoxColor = new Vector4(1, 1, 1, 1);

            control.UpdateModelMatrix(mtx * Matrix4.CreateTranslation(Vector3.UnitX * (3f - Math.Abs(control.RedrawerFrame * 0.0625f % 6f - 3f))));

            Renderers.ColorBlockRenderer.Draw(control, pass, Color, Color, pickingColor);

            control.UpdateModelMatrix(mtx);

            Renderers.ColorBlockRenderer.DrawLineBox(control, pass, lineBoxColor, pickingColor);

            control.UpdateModelMatrix(mtx * Matrix4.CreateTranslation(Vector3.UnitX * 3f));

            Renderers.ColorBlockRenderer.DrawLineBox(control, pass, lineBoxColor, pickingColor);
        }

        public override void Prepare(GL_ControlModern control)
        {
            Renderers.ColorBlockRenderer.Initialize(control);
            base.Prepare(control);
        }

        public override void Prepare(GL_ControlLegacy control)
        {
            Renderers.ColorBlockRenderer.Initialize(control);
            base.Prepare(control);
        }

        public override uint Select(int index, GL_ControlBase control, ISet<object> selectedObjects)
        {
            selectedObjects?.Add(this);
            if (!Selected)
            {
                Selected = true;
                control.AttachPickingRedrawer();
            }
            return 0;
        }

        public override uint SelectDefault(GL_ControlBase control, ISet<object> selectedObjects)
        {
            selectedObjects?.Add(this);
            if (!Selected)
            {
                Selected = true;
                control.AttachPickingRedrawer();
            }
            return 0;
        }

        public override uint SelectAll(GL_ControlBase control, ISet<object> selectedObjects)
        {
            selectedObjects?.Add(this);
            if (!Selected)
            {
                Selected = true;
                control.AttachPickingRedrawer();
            }
            return 0;
        }

        public override uint Deselect(int index, GL_ControlBase control, ISet<object> selectedObjects)
        {
            selectedObjects?.Remove(this);
            if (Selected)
            {
                Selected = false;
                control.DetachPickingRedrawer();
            }
            return 0;
        }

        public override uint DeselectAll(GL_ControlBase control, ISet<object> selectedObjects)
        {
            selectedObjects?.Remove(this);
            if (Selected)
            {
                Selected = false;
                control.DetachPickingRedrawer();
            }
            return 0;
        }
    }
}
