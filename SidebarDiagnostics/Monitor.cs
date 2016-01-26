﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows.Media;
using OpenHardwareMonitor.Hardware;

namespace SidebarDiagnostics.Monitor
{
    public class MonitorManager
    {
        public MonitorManager(IComputer computer)
        {
            _computer = computer;
            _board = GetHardware(HardwareType.Mainboard).FirstOrDefault();

            MonitorPanels = new List<MonitorPanel>();

            UpdateBoard();
        }

        public void AddPanel(MonitorConfig config)
        {
            switch (config.Type)
            {
                case MonitorType.CPU:
                    OHMPanel(
                        config.Type,
                        "M 19,19L 57,19L 57,22.063C 56.1374,22.285 55.5,23.0681 55.5,24C 55.5,24.9319 56.1374,25.715 57,25.937L 57,57L 19,57L 19,27.937C 19.8626,27.715 20.5,26.9319 20.5,26C 20.5,25.0681 19.8626,24.285 19,24.063L 19,19 Z M 21.9998,22.0005L 21.9998,24.0005L 23.9998,24.0005L 23.9998,22.0005L 21.9998,22.0005 Z M 24.9998,22.0005L 24.9998,24.0005L 26.9998,24.0005L 26.9998,22.0005L 24.9998,22.0005 Z M 27.9998,22.0005L 27.9998,24.0005L 29.9998,24.0005L 29.9998,22.0005L 27.9998,22.0005 Z M 30.9998,22.0005L 30.9998,24.0005L 32.9998,24.0005L 32.9998,22.0005L 30.9998,22.0005 Z M 33.9998,22.0005L 33.9998,24.0005L 35.9998,24.0005L 35.9998,22.0005L 33.9998,22.0005 Z M 36.9998,22.0005L 36.9998,24.0005L 38.9998,24.0005L 38.9998,22.0005L 36.9998,22.0005 Z M 39.9998,22.0005L 39.9998,24.0005L 41.9998,24.0005L 41.9998,22.0005L 39.9998,22.0005 Z M 42.9995,22.0005L 42.9995,24.0005L 44.9995,24.0005L 44.9995,22.0005L 42.9995,22.0005 Z M 45.9995,22.0005L 45.9995,24.0005L 47.9995,24.0005L 47.9995,22.0005L 45.9995,22.0005 Z M 48.9995,22.0004L 48.9995,24.0004L 50.9995,24.0004L 50.9995,22.0004L 48.9995,22.0004 Z M 51.9996,22.0004L 51.9996,24.0004L 53.9996,24.0004L 53.9996,22.0004L 51.9996,22.0004 Z M 21.9998,25.0004L 21.9998,27.0004L 23.9998,27.0004L 23.9998,25.0004L 21.9998,25.0004 Z M 24.9998,25.0004L 24.9998,27.0004L 26.9998,27.0004L 26.9998,25.0004L 24.9998,25.0004 Z M 27.9998,25.0004L 27.9998,27.0004L 29.9998,27.0004L 29.9998,25.0004L 27.9998,25.0004 Z M 30.9998,25.0004L 30.9998,27.0004L 32.9998,27.0004L 32.9998,25.0004L 30.9998,25.0004 Z M 33.9998,25.0004L 33.9998,27.0004L 35.9998,27.0004L 35.9998,25.0004L 33.9998,25.0004 Z M 36.9998,25.0004L 36.9998,27.0004L 38.9998,27.0004L 38.9998,25.0004L 36.9998,25.0004 Z M 39.9998,25.0004L 39.9998,27.0004L 41.9998,27.0004L 41.9998,25.0004L 39.9998,25.0004 Z M 42.9996,25.0004L 42.9996,27.0004L 44.9996,27.0004L 44.9996,25.0004L 42.9996,25.0004 Z M 45.9996,25.0004L 45.9996,27.0004L 47.9996,27.0004L 47.9996,25.0004L 45.9996,25.0004 Z M 48.9996,25.0004L 48.9996,27.0004L 50.9996,27.0004L 50.9996,25.0004L 48.9996,25.0004 Z M 51.9996,25.0004L 51.9996,27.0004L 53.9996,27.0004L 53.9996,25.0004L 51.9996,25.0004 Z M 21.9998,28.0004L 21.9998,30.0004L 23.9998,30.0004L 23.9998,28.0004L 21.9998,28.0004 Z M 24.9998,28.0004L 24.9998,30.0004L 26.9998,30.0004L 26.9998,28.0004L 24.9998,28.0004 Z M 27.9998,28.0004L 27.9998,30.0004L 29.9998,30.0004L 29.9998,28.0004L 27.9998,28.0004 Z M 30.9998,28.0004L 30.9998,30.0004L 32.9998,30.0004L 32.9998,28.0004L 30.9998,28.0004 Z M 33.9998,28.0004L 33.9998,30.0004L 35.9998,30.0004L 35.9998,28.0004L 33.9998,28.0004 Z M 36.9998,28.0004L 36.9998,30.0004L 38.9998,30.0004L 38.9998,28.0004L 36.9998,28.0004 Z M 39.9998,28.0004L 39.9998,30.0004L 41.9998,30.0004L 41.9998,28.0004L 39.9998,28.0004 Z M 42.9996,28.0004L 42.9996,30.0004L 44.9996,30.0004L 44.9996,28.0004L 42.9996,28.0004 Z M 45.9997,28.0004L 45.9997,30.0004L 47.9997,30.0004L 47.9997,28.0004L 45.9997,28.0004 Z M 48.9997,28.0003L 48.9997,30.0003L 50.9997,30.0003L 50.9997,28.0003L 48.9997,28.0003 Z M 51.9997,28.0003L 51.9997,30.0003L 53.9997,30.0003L 53.9997,28.0003L 51.9997,28.0003 Z M 21.9998,31.0003L 21.9998,33.0003L 23.9998,33.0003L 23.9998,31.0003L 21.9998,31.0003 Z M 24.9998,31.0003L 24.9998,33.0003L 26.9998,33.0003L 26.9998,31.0003L 24.9998,31.0003 Z M 27.9998,31.0003L 27.9998,33.0003L 29.9998,33.0003L 29.9998,31.0003L 27.9998,31.0003 Z M 45.9997,31.0003L 45.9997,33.0003L 47.9997,33.0003L 47.9997,31.0003L 45.9997,31.0003 Z M 48.9997,31.0003L 48.9997,33.0003L 50.9997,33.0003L 50.9997,31.0003L 48.9997,31.0003 Z M 51.9997,31.0003L 51.9997,33.0003L 53.9997,33.0003L 53.9997,31.0003L 51.9997,31.0003 Z M 21.9998,34.0001L 21.9998,36.0001L 23.9998,36.0001L 23.9998,34.0001L 21.9998,34.0001 Z M 24.9999,34.0001L 24.9999,36.0001L 26.9999,36.0001L 26.9999,34.0001L 24.9999,34.0001 Z M 27.9999,34.0001L 27.9999,36.0001L 29.9999,36.0001L 29.9999,34.0001L 27.9999,34.0001 Z M 45.9997,34.0001L 45.9997,36.0001L 47.9997,36.0001L 47.9997,34.0001L 45.9997,34.0001 Z M 48.9997,34.0001L 48.9997,36.0001L 50.9997,36.0001L 50.9997,34.0001L 48.9997,34.0001 Z M 51.9997,34.0001L 51.9997,36.0001L 53.9997,36.0001L 53.9997,34.0001L 51.9997,34.0001 Z M 21.9999,37.0001L 21.9999,39.0001L 23.9999,39.0001L 23.9999,37.0001L 21.9999,37.0001 Z M 24.9999,37.0001L 24.9999,39.0001L 26.9999,39.0001L 26.9999,37.0001L 24.9999,37.0001 Z M 27.9999,37.0001L 27.9999,39.0001L 29.9999,39.0001L 29.9999,37.0001L 27.9999,37.0001 Z M 45.9997,37.0001L 45.9997,39.0001L 47.9997,39.0001L 47.9997,37.0001L 45.9997,37.0001 Z M 48.9998,37.0001L 48.9998,39.0001L 50.9998,39.0001L 50.9998,37.0001L 48.9998,37.0001 Z M 51.9998,37.0001L 51.9998,39.0001L 53.9998,39.0001L 53.9998,37.0001L 51.9998,37.0001 Z M 21.9999,40.0001L 21.9999,42.0001L 23.9999,42.0001L 23.9999,40.0001L 21.9999,40.0001 Z M 24.9999,40.0001L 24.9999,42.0001L 26.9999,42.0001L 26.9999,40.0001L 24.9999,40.0001 Z M 27.9999,40.0001L 27.9999,42.0001L 29.9999,42.0001L 29.9999,40.0001L 27.9999,40.0001 Z M 45.9998,40.0001L 45.9998,42.0001L 47.9998,42.0001L 47.9998,40.0001L 45.9998,40.0001 Z M 48.9998,40.0001L 48.9998,42.0001L 50.9998,42.0001L 50.9998,40.0001L 48.9998,40.0001 Z M 51.9998,40.0001L 51.9998,42.0001L 53.9998,42.0001L 53.9998,40.0001L 51.9998,40.0001 Z M 21.9999,43.0001L 21.9999,45.0001L 23.9999,45.0001L 23.9999,43.0001L 21.9999,43.0001 Z M 24.9999,43.0001L 24.9999,45.0001L 26.9999,45.0001L 26.9999,43.0001L 24.9999,43.0001 Z M 27.9999,43.0001L 27.9999,45.0001L 29.9999,45.0001L 29.9999,43.0001L 27.9999,43.0001 Z M 45.9998,43.0001L 45.9998,45.0001L 47.9998,45.0001L 47.9998,43.0001L 45.9998,43.0001 Z M 48.9998,43.0001L 48.9998,45.0001L 50.9998,45.0001L 50.9998,43.0001L 48.9998,43.0001 Z M 51.9998,43.0001L 51.9998,45.0001L 53.9998,45.0001L 53.9998,43.0001L 51.9998,43.0001 Z M 21.9999,46.0001L 21.9999,48.0001L 23.9999,48.0001L 23.9999,46.0001L 21.9999,46.0001 Z M 24.9999,46.0001L 24.9999,48.0001L 26.9999,48.0001L 26.9999,46.0001L 24.9999,46.0001 Z M 27.9999,46.0001L 27.9999,48.0001L 29.9999,48.0001L 29.9999,46.0001L 27.9999,46.0001 Z M 30.9999,46.0001L 30.9999,48.0001L 32.9999,48.0001L 32.9999,46.0001L 30.9999,46.0001 Z M 33.9999,46.0001L 33.9999,48.0001L 35.9999,48.0001L 35.9999,46.0001L 33.9999,46.0001 Z M 36.9999,46.0001L 36.9999,48.0001L 38.9999,48.0001L 38.9999,46.0001L 36.9999,46.0001 Z M 39.9999,46.0001L 39.9999,48.0001L 41.9999,48.0001L 41.9999,46.0001L 39.9999,46.0001 Z M 42.9999,46.0001L 42.9999,48.0001L 44.9999,48.0001L 44.9999,46.0001L 42.9999,46.0001 Z M 45.9999,46.0001L 45.9999,48.0001L 47.9999,48.0001L 47.9999,46.0001L 45.9999,46.0001 Z M 48.9999,46.0001L 48.9999,48.0001L 50.9999,48.0001L 50.9999,46.0001L 48.9999,46.0001 Z M 51.9999,46.0001L 51.9999,48.0001L 53.9999,48.0001L 53.9999,46.0001L 51.9999,46.0001 Z M 21.9999,49.0001L 21.9999,51.0001L 23.9999,51.0001L 23.9999,49.0001L 21.9999,49.0001 Z M 24.9999,49.0001L 24.9999,51.0001L 26.9999,51.0001L 26.9999,49.0001L 24.9999,49.0001 Z M 27.9999,49.0001L 27.9999,51.0001L 29.9999,51.0001L 29.9999,49.0001L 27.9999,49.0001 Z M 30.9999,49.0001L 30.9999,51.0001L 33,51.0001L 33,49.0001L 30.9999,49.0001 Z M 34,49.0001L 34,51.0001L 36,51.0001L 36,49.0001L 34,49.0001 Z M 37,49.0001L 37,51.0001L 39,51.0001L 39,49.0001L 37,49.0001 Z M 40,49.0001L 40,51.0001L 42,51.0001L 42,49.0001L 40,49.0001 Z M 42.9999,49.0001L 42.9999,51.0001L 44.9999,51.0001L 44.9999,49.0001L 42.9999,49.0001 Z M 45.9999,49L 45.9999,51L 47.9999,51L 47.9999,49L 45.9999,49 Z M 48.9999,49L 48.9999,51L 50.9999,51L 50.9999,49L 48.9999,49 Z M 51.9999,49L 51.9999,51L 53.9999,51L 53.9999,49L 51.9999,49 Z M 22,52L 22,54L 24,54L 24,52L 22,52 Z M 25,52L 25,54L 27,54L 27,52L 25,52 Z M 28,52L 28,54L 30,54L 30,52L 28,52 Z M 31,52L 31,54L 33,54L 33,52L 31,52 Z M 34,52L 34,54L 36,54L 36,52L 34,52 Z M 37,52L 37,54L 39,54L 39,52L 37,52 Z M 40,52L 40,54L 42,54L 42,52L 40,52 Z M 43,52L 43,54L 45,54L 45,52L 43,52 Z M 46,52L 46,54L 48,54L 48,52L 46,52 Z M 49,52L 49,54L 51,54L 51,52L 49,52 Z M 52,52L 52,54L 54,54L 54,52L 52,52 Z M 31,31L 31,45L 45,45L 45,31L 31,31 Z M 33.6375,36.64L 33.4504,36.565L 33.3733,36.375L 33.4504,36.1829L 33.6375,36.1067L 33.8283,36.1829L 33.9067,36.375L 33.8283,36.5625L 33.6375,36.64 Z M 33.8533,40L 33.4266,40L 33.4266,37.3334L 33.8533,37.3334L 33.8533,40 Z M 36.9467,40L 36.52,40L 36.52,38.4942C 36.52,37.9336 36.3092,37.6533 35.8875,37.6533C 35.6697,37.6533 35.4896,37.7328 35.3471,37.8917C 35.2046,38.0506 35.1333,38.2514 35.1333,38.4942L 35.1333,40L 34.7066,40L 34.7066,37.3333L 35.1333,37.3333L 35.1333,37.7992L 35.1441,37.7992C 35.3486,37.4531 35.6444,37.28 36.0317,37.28C 36.3278,37.28 36.5543,37.3739 36.7112,37.5617C 36.8682,37.7495 36.9467,38.0206 36.9467,38.375L 36.9467,40 Z M 39.0267,39.9642L 38.6208,40.0533C 38.1447,40.0533 37.9067,39.7945 37.9067,39.2767L 37.9067,37.7067L 37.4267,37.7067L 37.4267,37.3333L 37.9067,37.3333L 37.9067,36.6733L 38.3333,36.5333L 38.3333,37.3333L 39.0267,37.3333L 39.0267,37.7067L 38.3333,37.7067L 38.3333,39.1892C 38.3333,39.3658 38.3647,39.4918 38.4275,39.5671C 38.4903,39.6424 38.5942,39.68 38.7392,39.68L 39.0267,39.5733L 39.0267,39.9642 Z M 41.6933,38.7733L 39.8267,38.7733C 39.8339,39.0628 39.9142,39.2863 40.0675,39.4438C 40.2208,39.6013 40.4319,39.68 40.7008,39.68C 41.003,39.68 41.2805,39.5911 41.5333,39.4133L 41.5333,39.8042C 41.3,39.9703 40.9911,40.0533 40.6067,40.0533C 40.2311,40.0533 39.9361,39.9331 39.7217,39.6925C 39.5072,39.452 39.4,39.1133 39.4,38.6767C 39.4,38.2645 39.516,37.9286 39.7479,37.6692C 39.9799,37.4097 40.268,37.28 40.6125,37.28C 40.9564,37.28 41.2225,37.3921 41.4108,37.6163C 41.5992,37.8404 41.6933,38.152 41.6933,38.5508L 41.6933,38.7733 Z M 41.2667,38.4C 41.265,38.1645 41.2058,37.9811 41.0892,37.85C 40.9725,37.7189 40.8103,37.6533 40.6025,37.6533C 40.4019,37.6533 40.2317,37.7222 40.0917,37.86C 39.9517,37.9978 39.8653,38.1778 39.8325,38.4L 41.2667,38.4 Z M 42.76,40L 42.3333,40L 42.3333,36.0533L 42.76,36.0533L 42.76,40 Z",
                        config.Params,
                        HardwareType.CPU
                        );
                    return;

                case MonitorType.RAM:
                    OHMPanel(
                        config.Type,
                        "M 473.00,193.00 C 473.00,193.00 434.00,193.00 434.00,193.00 434.00,193.00 434.00,245.00 434.00,245.00 434.00,245.00 259.00,245.00 259.00,245.00 259.00,239.01 259.59,235.54 256.67,230.00 247.91,213.34 228.26,212.83 217.65,228.00 213.65,233.71 214.00,238.44 214.00,245.00 214.00,245.00 27.00,245.00 27.00,245.00 27.00,245.00 27.00,193.00 27.00,193.00 27.00,193.00 0.00,193.00 0.00,193.00 0.00,193.00 0.00,20.00 0.00,20.00 12.36,19.43 21.26,13.56 18.00,0.00 18.00,0.00 453.00,0.00 453.00,0.00 453.01,7.85 454.03,15.96 463.00,18.82 465.56,19.42 470.18,19.04 473.00,18.82 473.00,18.82 473.00,193.00 473.00,193.00 Z M 433.00,39.00 C 433.00,39.00 386.00,39.00 386.00,39.00 386.00,39.00 386.00,147.00 386.00,147.00 386.00,147.00 433.00,147.00 433.00,147.00 433.00,147.00 433.00,39.00 433.00,39.00 Z M 423.00,193.00 C 423.00,193.00 399.00,193.00 399.00,193.00 399.00,193.00 399.00,224.00 399.00,224.00 399.00,224.00 387.00,224.00 387.00,224.00 387.00,224.00 387.00,193.00 387.00,193.00 387.00,193.00 377.00,193.00 377.00,193.00 377.00,193.00 377.00,224.00 377.00,224.00 377.00,224.00 365.00,224.00 365.00,224.00 365.00,224.00 365.00,193.00 365.00,193.00 365.00,193.00 354.00,193.00 354.00,193.00 354.00,193.00 354.00,224.00 354.00,224.00 354.00,224.00 343.00,224.00 343.00,224.00 343.00,224.00 343.00,193.00 343.00,193.00 343.00,193.00 333.00,193.00 333.00,193.00 333.00,193.00 333.00,224.00 333.00,224.00 333.00,224.00 322.00,224.00 322.00,224.00 322.00,224.00 322.00,193.00 322.00,193.00 322.00,193.00 311.00,193.00 311.00,193.00 311.00,193.00 311.00,224.00 311.00,224.00 311.00,224.00 300.00,224.00 300.00,224.00 300.00,224.00 300.00,193.00 300.00,193.00 300.00,193.00 289.00,193.00 289.00,193.00 289.00,193.00 289.00,224.00 289.00,224.00 289.00,224.00 277.00,224.00 277.00,224.00 277.00,224.00 277.00,193.00 277.00,193.00 277.00,193.00 191.00,193.00 191.00,193.00 191.00,193.00 191.00,224.00 191.00,224.00 191.00,224.00 179.00,224.00 179.00,224.00 179.00,224.00 179.00,193.00 179.00,193.00 179.00,193.00 169.00,193.00 169.00,193.00 169.00,193.00 169.00,224.00 169.00,224.00 169.00,224.00 157.00,224.00 157.00,224.00 157.00,224.00 157.00,193.00 157.00,193.00 157.00,193.00 146.00,193.00 146.00,193.00 146.00,193.00 146.00,224.00 146.00,224.00 146.00,224.00 134.00,224.00 134.00,224.00 134.00,224.00 134.00,193.00 134.00,193.00 134.00,193.00 125.00,193.00 125.00,193.00 125.00,193.00 125.00,224.00 125.00,224.00 125.00,224.00 114.00,224.00 114.00,224.00 114.00,224.00 114.00,193.00 114.00,193.00 114.00,193.00 103.00,193.00 103.00,193.00 103.00,193.00 103.00,224.00 103.00,224.00 103.00,224.00 91.00,224.00 91.00,224.00 91.00,224.00 91.00,193.00 91.00,193.00 91.00,193.00 81.00,193.00 81.00,193.00 81.00,193.00 81.00,224.00 81.00,224.00 81.00,224.00 69.00,224.00 69.00,224.00 69.00,224.00 69.00,193.00 69.00,193.00 69.00,193.00 39.00,193.00 39.00,193.00 39.00,193.00 39.00,234.00 39.00,234.00 39.00,234.00 203.00,234.00 203.00,234.00 204.62,218.32 219.49,205.67 235.00,205.04 245.28,204.62 255.94,209.24 262.67,217.04 265.14,219.89 267.13,223.51 268.54,227.00 269.28,228.84 269.93,231.78 271.56,232.98 273.27,234.24 276.91,234.00 279.00,234.00 279.00,234.00 423.00,234.00 423.00,234.00 423.00,234.00 423.00,193.00 423.00,193.00 Z M 367.00,39.00 C 367.00,39.00 320.00,39.00 320.00,39.00 320.00,39.00 320.00,147.00 320.00,147.00 320.00,147.00 367.00,147.00 367.00,147.00 367.00,147.00 367.00,39.00 367.00,39.00 Z M 303.00,39.00 C 303.00,39.00 256.00,39.00 256.00,39.00 256.00,39.00 256.00,147.00 256.00,147.00 256.00,147.00 303.00,147.00 303.00,147.00 303.00,147.00 303.00,39.00 303.00,39.00 Z M 215.00,39.00 C 215.00,39.00 168.00,39.00 168.00,39.00 168.00,39.00 168.00,147.00 168.00,147.00 168.00,147.00 215.00,147.00 215.00,147.00 215.00,147.00 215.00,39.00 215.00,39.00 Z M 148.00,39.00 C 148.00,39.00 101.00,39.00 101.00,39.00 101.00,39.00 101.00,147.00 101.00,147.00 101.00,147.00 148.00,147.00 148.00,147.00 148.00,147.00 148.00,39.00 148.00,39.00 Z M 84.00,39.00 C 84.00,39.00 37.00,39.00 37.00,39.00 37.00,39.00 37.00,147.00 37.00,147.00 37.00,147.00 84.00,147.00 84.00,147.00 84.00,147.00 84.00,39.00 84.00,39.00 Z",
                        config.Params,
                        HardwareType.RAM
                        );
                    return;

                case MonitorType.GPU:
                    OHMPanel(
                        config.Type,
                        "F1 M 20,23.0002L 55.9998,23.0002C 57.1044,23.0002 57.9998,23.8956 57.9998,25.0002L 57.9999,46C 57.9999,47.1046 57.1045,48 55.9999,48L 41,48L 41,53L 45,53C 46.1046,53 47,53.8954 47,55L 47,57L 29,57L 29,55C 29,53.8954 29.8955,53 31,53L 35,53L 35,48L 20,48C 18.8954,48 18,47.1046 18,46L 18,25.0002C 18,23.8956 18.8954,23.0002 20,23.0002 Z M 21,26.0002L 21,45L 54.9999,45L 54.9998,26.0002L 21,26.0002 Z",
                        config.Params,
                        HardwareType.GpuNvidia,
                        HardwareType.GpuAti
                        );
                    return;

                case MonitorType.HD:
                    DrivePanel(config.Type, config.Params);
                    return;

                case MonitorType.Network:
                    NetworkPanel(config.Type, config.Params);
                    return;
            }
        }

        public void Update()
        {
            UpdateBoard();

            foreach (iMonitor _monitor in MonitorPanels.SelectMany(p => p.Monitors))
            {
                _monitor.Update();
            }
        }

        private IEnumerable<IHardware> GetHardware(params HardwareType[] types)
        {
            return _computer.Hardware.Where(h => types.Contains(h.HardwareType));
        }

        private void OHMPanel(MonitorType type, string pathData, ConfigParam[] parameters, params HardwareType[] hardwareTypes)
        {
            MonitorPanel _monitorPanel = new MonitorPanel(type.GetDescription(), pathData);
            
            foreach (IHardware _hardware in GetHardware(hardwareTypes))
            {
                _monitorPanel.Add(new OHMMonitor(type, _hardware, _board, parameters));
            }

            MonitorPanels.Add(_monitorPanel);
        }

        private void DrivePanel(MonitorType type, ConfigParam[] parameters)
        {
            MonitorPanel _monitorPanel = new MonitorPanel(
                type.GetDescription(),
                "M 17.30,0.77 C 17.30,0.77 28.12,0.77 28.12,0.77 28.12,0.77 60.79,0.77 60.79,0.77 68.68,0.76 73.39,6.12 73.40,13.87 73.40,13.87 73.40,61.84 73.40,61.84 73.40,61.84 73.40,88.31 73.40,88.31 73.32,95.25 68.55,100.74 61.46,100.75 61.46,100.75 12.12,100.75 12.12,100.75 5.26,100.74 0.42,95.28 0.40,88.53 0.40,88.53 0.40,22.47 0.40,22.47 0.40,22.47 0.40,15.91 0.40,15.91 0.40,11.04 0.35,7.57 4.24,3.94 5.01,3.21 5.99,2.54 6.94,2.06 8.46,1.30 9.58,1.09 11.22,0.77 11.22,0.77 17.30,0.77 17.30,0.77 Z M 21.14,9.43 C 21.92,9.69 26.51,9.57 27.67,9.57 27.67,9.57 50.87,9.57 50.87,9.57 51.51,9.57 52.32,9.62 52.89,9.31 53.92,8.74 54.05,7.48 53.09,6.77 52.53,6.34 51.77,6.41 51.10,6.44 51.10,6.44 31.27,6.44 31.27,6.44 31.27,6.44 21.14,6.44 21.14,6.44 19.63,7.17 19.68,8.94 21.14,9.43 Z M 7.69,25.25 C 4.40,28.65 7.47,34.30 12.12,33.48 16.13,32.78 17.48,27.79 14.56,25.03 13.26,23.81 11.77,23.69 10.09,23.91 9.14,24.18 8.39,24.53 7.69,25.25 Z M 59.44,24.82 C 55.67,27.91 58.21,34.41 63.49,33.48 66.89,32.88 68.46,28.64 66.50,25.86 65.22,24.05 63.54,23.67 61.46,23.90 60.66,24.09 60.10,24.28 59.44,24.82 Z M 25.64,32.07 C 17.25,35.67 11.13,42.35 8.65,51.20 8.18,52.88 7.64,55.59 7.62,57.31 7.56,62.48 7.75,65.76 9.74,70.66 13.30,79.41 20.86,85.68 29.92,87.94 31.42,88.31 34.02,88.74 35.55,88.76 40.84,88.82 44.67,88.14 49.52,85.95 52.72,84.50 55.63,82.20 58.08,79.71 60.38,77.37 62.21,74.55 63.56,71.56 69.22,59.05 65.62,44.26 54.70,35.87 51.76,33.61 48.36,31.96 44.79,30.94 41.52,30.00 37.37,29.68 33.98,30.00 30.96,30.44 28.49,30.84 25.64,32.07 Z M 39.16,50.68 C 46.63,52.49 48.08,63.05 41.19,66.92 39.60,67.81 38.22,67.96 36.45,67.94 30.35,67.87 26.41,61.27 28.77,55.73 30.22,52.31 32.76,50.91 36.23,50.39 37.22,50.29 38.19,50.44 39.16,50.68 Z M 15.88,58.90 C 15.88,58.90 16.27,62.74 16.27,62.74 16.67,65.06 17.58,67.49 18.75,69.53 21.48,74.29 26.01,77.88 31.27,79.39 32.52,79.74 33.81,80.00 35.10,80.12 35.88,80.20 36.89,80.06 37.57,80.43 39.14,81.26 38.86,83.41 37.13,83.73 35.38,84.06 31.41,83.29 29.70,82.73 23.12,80.57 17.81,76.32 14.78,69.98 13.36,67.01 12.90,64.59 12.48,61.38 12.26,59.72 12.01,58.12 13.92,57.48 14.90,57.51 15.62,57.87 15.88,58.90 Z M 12.12,94.54 C 15.89,93.83 17.48,89.11 14.78,86.31 13.44,84.92 11.90,84.79 10.09,84.99 3.87,86.53 6.04,95.68 12.12,94.54 Z M 62.14,94.62 C 66.64,94.93 69.16,89.28 65.90,86.16 64.53,84.86 63.01,84.76 61.24,85.00 55.71,86.67 56.99,94.26 62.14,94.62 Z"
                );

            _monitorPanel.Add(new DriveMonitor(parameters));

            MonitorPanels.Add(_monitorPanel);
        }

        private void NetworkPanel(MonitorType type, ConfigParam[] parameters)
        {
            MonitorPanel _monitorPanel = new MonitorPanel(
                type.GetDescription(),
                "M 40,44L 39.9999,51L 44,51C 45.1046,51 46,51.8954 46,53L 46,57C 46,58.1046 45.1045,59 44,59L 32,59C 30.8954,59 30,58.1046 30,57L 30,53C 30,51.8954 30.8954,51 32,51L 36,51L 36,44L 40,44 Z M 47,53L 57,53L 57,57L 47,57L 47,53 Z M 29,53L 29,57L 19,57L 19,53L 29,53 Z M 19,22L 57,22L 57,31L 19,31L 19,22 Z M 55,24L 53,24L 53,29L 55,29L 55,24 Z M 51,24L 49,24L 49,29L 51,29L 51,24 Z M 47,24L 45,24L 45,29L 47,29L 47,24 Z M 21,27L 21,29L 23,29L 23,27L 21,27 Z M 19,33L 57,33L 57,42L 19,42L 19,33 Z M 55,35L 53,35L 53,40L 55,40L 55,35 Z M 51,35L 49,35L 49,40L 51,40L 51,35 Z M 47,35L 45,35L 45,40L 47,40L 47,35 Z M 21,38L 21,40L 23,40L 23,38L 21,38 Z"
                );

            _monitorPanel.Add(new NetworkMonitor(parameters));

            MonitorPanels.Add(_monitorPanel);
        }

        private void UpdateBoard()
        {
            _board.Update();

            foreach (IHardware _subhardware in _board.SubHardware)
            {
                _subhardware.Update();
            }
        }

        public List<MonitorPanel> MonitorPanels { get; private set; }

        private IComputer _computer { get; set; }

        private IHardware _board { get; set; }
    }

    public class MonitorPanel
    {
        public MonitorPanel(string title, string iconData)
        {
            IconPath = Geometry.Parse(iconData);
            Title = title;

            Monitors = new List<iMonitor>();
        }

        public void Add(iMonitor monitor)
        {
            Monitors.Add(monitor);
        }
        
        public Geometry IconPath { get; private set; }

        public string Title { get; private set; }

        public List<iMonitor> Monitors { get; private set; }
    }

    public interface iMonitor
    {
        void Update();
    }
    
    public class OHMMonitor : iMonitor
    {
        public OHMMonitor(MonitorType type, IHardware hardware, IHardware board, ConfigParam[] parameters)
        {
            Name = hardware.Name;

            ShowName = parameters.GetValue<bool>(ParamKey.HardwareNames);

            _hardware = hardware;

            UpdateHardware();

            switch (type)
            {
                case MonitorType.CPU:
                    InitCPU(
                        board,
                        parameters.GetValue<bool>(ParamKey.AllCoreClocks),
                        parameters.GetValue<bool>(ParamKey.CoreLoads),
                        parameters.GetValue<bool>(ParamKey.UseFahrenheit),
                        parameters.GetValue<int>(ParamKey.TempAlert)
                        );
                    break;

                case MonitorType.RAM:
                    InitRAM(
                        board
                        );
                    break;

                case MonitorType.GPU:
                    InitGPU(
                        parameters.GetValue<bool>(ParamKey.UseFahrenheit),
                        parameters.GetValue<int>(ParamKey.TempAlert)
                        );
                    break;
            }
        }

        public void Update()
        {
            UpdateHardware();

            foreach (OHMSensor _sensor in Sensors)
            {
                _sensor.Update();
            }
        }

        private void UpdateHardware()
        {
            _hardware.Update();

            foreach (IHardware _subHardware in _hardware.SubHardware)
            {
                _subHardware.Update();
            }
        }

        private void InitCPU(IHardware board, bool allCoreClocks, bool coreLoads, bool useFahrenheit, double tempAlert)
        {
            List<OHMSensor> _sensorList = new List<OHMSensor>();

            ISensor[] _coreClocks = _hardware.Sensors.Where(s => s.SensorType == SensorType.Clock && s.Name.Contains("CPU")).ToArray();

            if (_coreClocks.Length > 0)
            {
                if (allCoreClocks)
                {
                    for (int i = 1; i <= _coreClocks.Max(s => s.Index); i++)
                    {
                        ISensor _coreClock = _coreClocks.Where(s => s.Index == i).FirstOrDefault();

                        if (_coreClock != null)
                        {
                            _sensorList.Add(new OHMSensor(_coreClock, DataType.Clock, string.Format("Core {0}", i - 1), true));
                        }
                    }
                }
                else
                {
                    ISensor _firstClock = _coreClocks.FirstOrDefault(c => c.Index == 1);

                    if (_firstClock != null)
                    {
                        _sensorList.Add(new OHMSensor(_firstClock, DataType.Clock, "Clock", true));
                    }
                }
            }

            ISensor _voltage = null;
            ISensor _tempSensor = null;
            ISensor _fanSensor = null;

            if (board != null)
            {
                _voltage = board.Sensors.Where(s => s.SensorType == SensorType.Voltage && s.Name.Contains("CPU")).FirstOrDefault();
                _tempSensor = board.Sensors.Where(s => s.SensorType == SensorType.Temperature && s.Name.Contains("CPU")).FirstOrDefault();
                _fanSensor = board.Sensors.Where(s => new SensorType[2] { SensorType.Fan, SensorType.Control }.Contains(s.SensorType) && s.Name.Contains("CPU")).FirstOrDefault();
            }

            if (_voltage == null)
            {
                _voltage = _hardware.Sensors.Where(s => s.SensorType == SensorType.Voltage).FirstOrDefault();
            }

            if (_tempSensor == null)
            {
                _tempSensor =
                    _hardware.Sensors.Where(s => s.SensorType == SensorType.Temperature && s.Name == "CPU Package").FirstOrDefault() ??
                    _hardware.Sensors.Where(s => s.SensorType == SensorType.Temperature).FirstOrDefault();
            }

            if (_fanSensor == null)
            {
                _fanSensor = _hardware.Sensors.Where(s => new SensorType[2] { SensorType.Fan, SensorType.Control }.Contains(s.SensorType)).FirstOrDefault();
            }

            if (_voltage != null)
            {
                _sensorList.Add(new OHMSensor(_voltage, DataType.Voltage, "Volt"));
            }

            if (_tempSensor != null)
            {
                _sensorList.Add(new OHMSensor(_tempSensor, DataType.Celcius, "Temp", false, tempAlert, (useFahrenheit ? CelciusToFahrenheit.Instance : null)));
            }

            if (_fanSensor != null)
            {
                _sensorList.Add(new OHMSensor(_fanSensor, DataType.RPM, "Fan"));
            }

            ISensor[] _loadSensors = _hardware.Sensors.Where(s => s.SensorType == SensorType.Load).ToArray();

            if (_loadSensors.Length > 0)
            {
                ISensor _totalCPU = _loadSensors.Where(s => s.Index == 0).FirstOrDefault();

                if (_totalCPU != null)
                {
                    _sensorList.Add(new OHMSensor(_totalCPU, DataType.Percent, "Load"));
                }

                if (coreLoads)
                {
                    for (int i = 1; i <= _loadSensors.Max(s => s.Index); i++)
                    {
                        ISensor _coreLoad = _loadSensors.Where(s => s.Index == i).FirstOrDefault();

                        if (_coreLoad != null)
                        {
                            _sensorList.Add(new OHMSensor(_coreLoad, DataType.Percent, string.Format("Core {0}", i - 1)));
                        }
                    }
                }
            }

            Sensors = _sensorList.ToArray();
        }

        public void InitRAM(IHardware board)
        {
            List<OHMSensor> _sensorList = new List<OHMSensor>();

            ISensor _ramClock = _hardware.Sensors.Where(s => s.SensorType == SensorType.Clock).FirstOrDefault();

            if (_ramClock != null)
            {
                _sensorList.Add(new OHMSensor(_ramClock, DataType.Clock, "Clock", true));
            }

            ISensor _voltage = null;

            if (board != null)
            {
                _voltage = board.Sensors.Where(s => s.SensorType == SensorType.Voltage && s.Name.Contains("RAM")).FirstOrDefault();
            }

            if (_voltage == null)
            {
                _voltage = _hardware.Sensors.Where(s => s.SensorType == SensorType.Voltage).FirstOrDefault();
            }

            if (_voltage != null)
            {
                _sensorList.Add(new OHMSensor(_voltage, DataType.Voltage, "Volt"));
            }

            ISensor _loadSensor = _hardware.Sensors.Where(s => s.SensorType == SensorType.Load && s.Index == 0).FirstOrDefault();

            if (_loadSensor != null)
            {
                _sensorList.Add(new OHMSensor(_loadSensor, DataType.Percent, "Load"));
            }

            ISensor _usedSensor = _hardware.Sensors.Where(s => s.SensorType == SensorType.Data && s.Index == 0).FirstOrDefault();

            if (_usedSensor != null)
            {
                _sensorList.Add(new OHMSensor(_usedSensor, DataType.Gigabyte, "Used"));
            }

            ISensor _availSensor = _hardware.Sensors.Where(s => s.SensorType == SensorType.Data && s.Index == 1).FirstOrDefault();

            if (_availSensor != null)
            {
                _sensorList.Add(new OHMSensor(_availSensor, DataType.Gigabyte, "Free"));
            }

            Sensors = _sensorList.ToArray();
        }

        public void InitGPU(bool useFahrenheit, double tempAlert)
        {
            List<OHMSensor> _sensorList = new List<OHMSensor>();

            ISensor _coreClock = _hardware.Sensors.Where(s => s.SensorType == SensorType.Clock && s.Index == 0).FirstOrDefault();

            if (_coreClock != null)
            {
                _sensorList.Add(new OHMSensor(_coreClock, DataType.Clock, "Core", true));
            }

            ISensor _memoryClock = _hardware.Sensors.Where(s => s.SensorType == SensorType.Clock && s.Index == 1).FirstOrDefault();

            if (_memoryClock != null)
            {
                _sensorList.Add(new OHMSensor(_memoryClock, DataType.Clock, "VRAM", true));
            }

            ISensor _coreLoad = _hardware.Sensors.Where(s => s.SensorType == SensorType.Load && s.Index == 0).FirstOrDefault();

            if (_coreLoad != null)
            {
                _sensorList.Add(new OHMSensor(_coreLoad, DataType.Percent, "Core"));
            }

            ISensor _memoryLoad = _hardware.Sensors.Where(s => s.SensorType == SensorType.Load && s.Index == 3).FirstOrDefault();

            if (_memoryLoad != null)
            {
                _sensorList.Add(new OHMSensor(_memoryLoad, DataType.Percent, "VRAM"));
            }

            ISensor _tempSensor = _hardware.Sensors.Where(s => s.SensorType == SensorType.Temperature && s.Index == 0).FirstOrDefault();

            if (_tempSensor != null)
            {
                _sensorList.Add(new OHMSensor(_tempSensor, DataType.Celcius, "Temp", false, tempAlert, (useFahrenheit ? CelciusToFahrenheit.Instance : null)));
            }

            ISensor _fanSensor = _hardware.Sensors.Where(s => s.SensorType == SensorType.Control && s.Index == 0).FirstOrDefault();

            if (_fanSensor != null)
            {
                _sensorList.Add(new OHMSensor(_fanSensor, DataType.Percent, "Fan"));
            }

            Sensors = _sensorList.ToArray();
        }

        public string Name { get; private set; }

        public bool ShowName { get; private set; }

        public OHMSensor[] Sensors { get; private set; }

        private IHardware _hardware { get; set; }
    }

    public class OHMSensor : INotifyPropertyChanged
    {
        public OHMSensor(ISensor sensor, DataType dataType, string label, bool round = false, double alertValue = 0, iConverter converter = null)
        {
            _sensor = sensor;
            _converter = converter;

            if (_converter == null)
            {
                DataType = dataType;
                Append = dataType.GetAppend();
            }
            else
            {
                DataType = converter.TargetType;
                Append = converter.TargetType.GetAppend();
            }

            Label = label;
            Round = round;
            AlertValue = alertValue;
        }

        public void Update()
        {
            if (_sensor.Value.HasValue)
            {
                double _value = _sensor.Value.Value;

                if (_converter != null)
                {
                    _converter.Convert(ref _value);
                }

                if (Round)
                {
                    _value = Math.Round(_value);
                }

                if (AlertValue > 0 && AlertValue <= _value)
                {
                    if (!IsAlert)
                    {
                        IsAlert = true;
                    }
                }
                else if (IsAlert)
                {
                    IsAlert = false;
                }

                Text = string.Format(
                    "{0}: {1:0.##}{2}",
                    Label,
                    _value,
                    Append
                    );
            }
            else
            {
                Text = string.Format("{0}: No Value", Label);
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler _handler = PropertyChanged;

            if (_handler != null)
            {
                _handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _text { get; set; }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;

                NotifyPropertyChanged("Text");
            }
        }

        public DataType DataType { get; private set; }

        public string Label { get; set; }

        public string Append { get; private set; }
        
        public bool Round { get; set; }

        public double AlertValue { get; private set; }

        private bool _isAlert { get; set; } = false;

        public bool IsAlert
        {
            get
            {
                return _isAlert;
            }
            set
            {
                _isAlert = value;

                NotifyPropertyChanged("IsAlert");
            }
        }

        private ISensor _sensor { get; set; }

        private iConverter _converter { get; set; }
    }

    public class DriveMonitor : iMonitor, INotifyPropertyChanged
    {
        public DriveMonitor(ConfigParam[] parameters)
        {
            UsedSpaceAlert = parameters.GetValue<int>(ParamKey.UsedSpaceAlert);
        }

        public void Update()
        {
            // cooldown so we don't use up too much CPU and drive resources

            DateTime _now = DateTime.Now;

            if ((_now - _lastUpdate).Minutes < 2)
                return;

            _lastUpdate = _now;

            Drives = DriveInfo.GetDrives().Select(d => new DriveData(d, UsedSpaceAlert)).ToArray();
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler _handler = PropertyChanged;

            if (_handler != null)
            {
                _handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private DriveData[] _drives { get; set; }

        public DriveData[] Drives
        {
            get
            {
                return _drives;
            }
            set
            {
                _drives = value;

                NotifyPropertyChanged("Drives");
            }
        }

        public double UsedSpaceAlert { get; private set; }

        private DateTime _lastUpdate { get; set; }
    }

    public class DriveData
    {
        public DriveData(DriveInfo driveInfo, double usedSpaceAlert)
        {
            Label = string.Format("{0} {1}", driveInfo.Name, driveInfo.VolumeLabel);

            double _freeGB = driveInfo.AvailableFreeSpace / 1073741824d;
            double _totalGB = driveInfo.TotalSize / 1073741824d;
            double _usedGB = (_totalGB - _freeGB);
            double _load = _usedGB / _totalGB * 100d;

            Load = string.Format("Load: {0:0.##}%", _load);
            UsedGB = string.Format("Used: {0:0.##} GB", _usedGB);
            FreeGB = string.Format("Free: {0:0.##} GB", _freeGB);

            IsAlert = usedSpaceAlert > 0 && _load > usedSpaceAlert;
        }
        
        public string Label { get; private set; }

        public string Load { get; private set; }

        public string UsedGB { get; private set; }

        public string FreeGB { get; private set; }

        public bool IsAlert { get; private set; }
    }

    public class NetworkMonitor : iMonitor
    {
        public NetworkMonitor(ConfigParam[] parameters)
        {
            Nics = NetworkInterface.GetAllNetworkInterfaces().Where(n => n.OperationalStatus == OperationalStatus.Up && new NetworkInterfaceType[2] { NetworkInterfaceType.Ethernet, NetworkInterfaceType.Wireless80211 }.Contains(n.NetworkInterfaceType)).Select(n => new NicData(n.Description)).ToArray();
        }

        public void Update()
        {
            foreach (NicData _nic in Nics)
            {
                _nic.Update();
            }
        }
        
        public NicData[] Nics { get; private set; }
    }

    public class NicData : INotifyPropertyChanged
    {
        public NicData(string name)
        {
            Name = name;

            _outCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", name);
            _inCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", name);
        }

        public void Update()
        {
            OutKB = string.Format("Up: {0:0.##} Kb/s", _outCounter.NextValue() / 128d);
            InKB = string.Format("Down: {0:0.##} Kb/s", _inCounter.NextValue() / 128d);
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler _handler = PropertyChanged;

            if (_handler != null)
            {
                _handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; private set; }

        private PerformanceCounter _outCounter { get; set; }

        private string _outKB { get; set; }

        public string OutKB
        {
            get
            {
                return _outKB;
            }
            set
            {
                _outKB = value;

                NotifyPropertyChanged("OutKB");
            }
        }

        private PerformanceCounter _inCounter { get; set; }

        private string _inKB { get; set; }

        public string InKB
        {
            get
            {
                return _inKB;
            }
            set
            {
                _inKB = value;

                NotifyPropertyChanged("InKB");
            }
        }
    }

    [Serializable]
    public enum MonitorType : byte
    {
        CPU,
        RAM,
        GPU,
        HD,
        Network
    }
    
    [Serializable]
    public class MonitorConfig
    {
        public MonitorType Type { get; set; }

        public bool Enabled { get; set; }

        public byte Order { get; set; }

        public ConfigParam[] Params { get; set; }

        public string Name
        {
            get
            {
                return Type.GetDescription();
            }
        }
    }

    [Serializable]
    public class ConfigParam
    {
        public ParamKey Key { get; set; }

        public object Value { get; set; }

        public string Type
        {
            get
            {
                return Value.GetType().ToString();
            }
        }

        public string Name
        {
            get
            {
                switch (Key)
                {
                    case ParamKey.HardwareNames:
                        return "Show Hardware Names";

                    case ParamKey.UseFahrenheit:
                        return "Use Fahrenheit";

                    case ParamKey.AllCoreClocks:
                        return "Show All Core Clocks";

                    case ParamKey.CoreLoads:
                        return "Show Core Loads";

                    case ParamKey.TempAlert:
                        return "Temperature Alert";

                    case ParamKey.UsedSpaceAlert:
                        return "% Used Alert";

                    default:
                        return "Unknown";
                }
            }
        }

        public static class Defaults
        {
            public static ConfigParam HardwareNames
            {
                get
                {
                    return new ConfigParam() { Key = ParamKey.HardwareNames, Value = true };
                }
            }

            public static ConfigParam NoHardwareNames
            {
                get
                {
                    return new ConfigParam() { Key = ParamKey.HardwareNames, Value = false };
                }
            }

            public static ConfigParam UseFahrenheit
            {
                get
                {
                    return new ConfigParam() { Key = ParamKey.UseFahrenheit, Value = false };
                }
            }

            public static ConfigParam AllCoreClocks
            {
                get
                {
                    return new ConfigParam() { Key = ParamKey.AllCoreClocks, Value = false };
                }
            }

            public static ConfigParam CoreLoads
            {
                get
                {
                    return new ConfigParam() { Key = ParamKey.CoreLoads, Value = true };
                }
            }

            public static ConfigParam TempAlert
            {
                get
                {
                    return new ConfigParam() { Key = ParamKey.TempAlert, Value = 0 };
                }
            }

            public static ConfigParam UsedSpaceAlert
            {
                get
                {
                    return new ConfigParam() { Key = ParamKey.UsedSpaceAlert, Value = 0 };
                }
            }
        }
    }

    [Serializable]
    public enum ParamKey : byte
    {
        HardwareNames,
        UseFahrenheit,
        AllCoreClocks,
        CoreLoads,
        TempAlert,
        UsedSpaceAlert
    }

    public enum DataType : byte
    {
        Clock,
        Voltage,
        Percent,
        RPM,
        Celcius,
        Fahrenheit,
        Gigabyte
    }

    public interface iConverter
    {
        void Convert(ref double value);

        DataType TargetType { get; }
    }

    public class CelciusToFahrenheit : iConverter
    {
        private CelciusToFahrenheit() { }

        public void Convert(ref double value)
        {
            value = value * 1.8 + 32;
        }

        public DataType TargetType
        {
            get
            {
                return DataType.Fahrenheit;
            }
        }

        private static CelciusToFahrenheit _instance { get; set; }

        public static CelciusToFahrenheit Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CelciusToFahrenheit();
                }

                return _instance;
            }
        }
    }

    public static class Extensions
    {
        public static string GetDescription(this MonitorType type)
        {
            switch (type)
            {
                case MonitorType.CPU:
                    return "CPU";

                case MonitorType.RAM:
                    return "RAM";

                case MonitorType.GPU:
                    return "GPU";

                case MonitorType.HD:
                    return "Drives";

                case MonitorType.Network:
                    return "Network";

                default:
                    return "Unknown";
            }
        }

        public static T GetValue<T>(this ConfigParam[] parameters, ParamKey key)
        {
            return (T)parameters.Single(p => p.Key == key).Value;
        }

        public static string GetAppend(this DataType type)
        {
            switch (type)
            {
                case DataType.Clock:
                    return " MHz";

                case DataType.Voltage:
                    return " V";

                case DataType.Percent:
                    return "%";

                case DataType.RPM:
                    return " RPM";

                case DataType.Celcius:
                    return " C";

                case DataType.Fahrenheit:
                    return " F";

                case DataType.Gigabyte:
                    return " GB";

                default:
                    return "";
            }
        }
    }
}
