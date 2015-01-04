/*
 * Author: dtobi, Firov
 * This work is shared under CC BY-NC-ND 3.0 license.
 * Non commercial, no derivatives, attribution if shared unmodified.
 * You may distribute this code and the compiled .dll as is.
 * 
 * Exception from the no-deriviates clause in case of KSP updates:
 * In case of an update of KSP that breaks the code, you may change
 * this code to make it work again and redistribute it under a different
 * class name until the author publishes an updated version. After a
 * release by the author, the right to redistribute the changed code
 * vanishes.
 * 
 * You must keep this boilerplate in the file and give credit to the author
 * in the download file as well as on the webiste that links to the file.
 * 
 * Should you wish to change things in the code, contact me via the KSP forum.
 * Patches are welcome.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.IO;

namespace KM_Lib
{


    public static class Utility
    {


        public static void switchLight(Part p, String lightName, bool state) {
            Transform lightTransform = p.FindModelTransform(lightName);
            if (lightTransform != null) {
                Light light = lightTransform.GetComponent<Light>();
                if (light != null)
                    light.intensity = (state ? 1 : 0);
            }

        }

        private static void traverseChildren(Part p, int nextStage, ref List<Part> resultList) {
            if (p.inverseStage >= nextStage) {
                resultList.Add(p);
            }
            foreach (Part child in p.children) {

                traverseChildren(child, nextStage, ref resultList);
            }
        }


        public static void playAnimation(Part p, string animationName, bool forward, bool play, float speed) {
            Animation anim;
            anim = p.FindModelAnimators(animationName).FirstOrDefault();
            if (anim != null) {


                if (forward) {
                    anim[animationName].speed = 1f * speed;
                    //PartModule.print ("NTime forward: " + anim [animationName].normalizedTime);
                    if (!play || !anim.isPlaying)
                        anim[animationName].normalizedTime = (play ? 0f : 1f);
                    anim.Blend(animationName, 2f);
                }
                else {
                    anim[animationName].speed = -1f * speed;
                    //PartModule.print ("NTime backward: " + anim [animationName].normalizedTime);
                    if (!play || !anim.isPlaying)
                        anim[animationName].normalizedTime = (play ? 1f : 0f);
                    anim.Blend(animationName, 2f);
                }
            }
        }

        public static void playAnimationSetToPosition(Part p, string animationName, float position) {
            Animation anim;
            anim = p.FindModelAnimators(animationName).FirstOrDefault();
            if (anim != null) {
                anim[animationName].normalizedTime = position;
                anim[animationName].speed = 0f;
                anim.Play(animationName);
                // anim [animationName].speed = 0;
            }
        }


        public static void playAudio(Part p, string clipName) {
            if (clipName != "") {
                AudioSource sound;
                sound = p.gameObject.AddComponent<AudioSource>();
                sound.clip = GameDatabase.Instance.GetAudioClip(clipName);
                if (sound.clip != null) {
                    sound.volume = 1;
                    sound.Stop();
                    sound.Play();
                }
            }
        }
    }
}










