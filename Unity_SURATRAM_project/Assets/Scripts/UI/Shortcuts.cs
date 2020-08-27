/*
* MIT License
*
* Copyright (c) 2017 Philip Tibom, Jonathan Jansson, Rickard Laurenius,
* Tobias Alldén, Martin Chemander, Sherry Davar
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

//using System;
using UnityEngine;

/// <summary>
/// Controlls all switching of views and UI elements depending on Play/Stop state.
/// Also invokes events to allert other scripts to act when toggling Play/Stop
///
/// @author: Jonathan Jansson
/// Modified by Jacques PEREIRA
/// </summary>
public static class Shortcuts
{
    private static bool isOn = false;

    public static event PlayToggledDelegate OnPlayToggled;
    public delegate void PlayToggledDelegate(bool toggleIsOn);

    static Shortcuts()
    {
        // gameObject.GetComponent<Toggle>().onValueChanged.AddListener(OnToggle);
    }

    public static void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            isOn = !isOn;
            OnPlayToggled(isOn);
        }
    }
}
