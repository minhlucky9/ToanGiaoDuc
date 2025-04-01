using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace MathCounting {
    public class ConstAnimator {
        public static string HELLO = "hello";
        public static string IDLE = "idle";
        public static string RIGHT = "reaction right";
        public static string WRONG = "reaction wrong";
        public static string TALK = "talk";
        public static string GUIDE_TALK = "guide talk";
        public static string GUIDE_ARM = "guide armgesture";

    }

    public enum AnimationName {

        [Description("idle")]
        Idle,

        [Description("hello")]
        Hello,

        [Description("reaction right")]
        ReactionRight,

        [Description("reaction wrong")]
        ReactionWrong,

        [Description("talk")]
        Talk,

        [Description("guide talk")]
        GuideTalk,

        [Description("guide armgesture")]
        GuideArmgesture
    }
    public static class EnumExtensions {
        public static string GetDescription( this Enum value ) {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }

}
