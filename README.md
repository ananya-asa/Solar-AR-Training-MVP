# 📱 Solar AR Training

<div align="center">

![Unity](https://img.shields.io/badge/Unity-2022.3.62f3-black?style=for-the-badge&logo=unity)
![AR Foundation](https://img.shields.io/badge/AR%20Foundation-5.1-blue?style=for-the-badge)
![Platform](https://img.shields.io/badge/Platform-Android-green?style=for-the-badge&logo=android)
![Status](https://img.shields.io/badge/Status-MVP%20Complete-success?style=for-the-badge)

**An AR-powered mobile training application for solar panel installation professionals**

[📥 Download APK](#-download) • [✨ Features](#-features) • [🎮 Usage](#-usage)

</div>

---

## 🌟 **Overview**

Solar AR Training is an augmented reality mobile application that trains solar installation professionals on optimal panel placement, tilt angle adjustment, and installation best practices. The app combines real-world AR visualization with **NASA-powered solar data** to provide location-specific training.

### **🎯 Problem Statement**
Improper solar panel installation (incorrect tilt angles, poor orientation) can result in **20-30% energy efficiency loss**. Traditional training methods lack hands-on, real-world practice opportunities.

### **✅ Our Solution**
An interactive AR training app that allows users to:
- 🎯 Practice panel placement in real environments
- 📐 Receive real-time feedback on tilt angles
- 🌍 Validate installations against NASA solar data
- 📝 Complete knowledge assessments
- 🎓 Earn digital certificates

---

## 📥 **Download**

### **Latest Release: v1.0.0**

[![Download APK](https://img.shields.io/badge/Download-APK-brightgreen?style=for-the-badge&logo=android)](https://github.com/ananya-asa/Solar-AR-Training-MVP/releases/tag/v2.0)

**Requirements**: Android 7.0+, ARCore compatible device ([Check compatibility](https://developers.google.com/ar/devices))

---

## ✨ **Features**

### **🔷 AR Panel Placement**
- Real-time surface detection using ARCore
- One-tap panel placement on detected planes
- Smart single panel management (move, not duplicate)
- Intuitive touch-based tilt adjustment

### **🔷 Live Tilt Validation**
- Real-time angle calculation and display
- Location-based optimal tilt recommendations
- Color-coded feedback system:
  - 🟢 **Green**: Optimal angle (±3° tolerance)
  - 🟡 **Yellow**: Needs adjustment
- Visual angle indicator with laser line

### **🔷 NASA API Integration**
- 📡 GPS-based location detection
- 🛰️ NASA POWER API for solar irradiance data
- 🔄 Automatic fallback to formula-based calculation (offline)
- 📍 Location-specific recommendations (e.g., 12.9° for Mangaluru, India)

### **🔷 Knowledge Assessment System**
- ✅ AR-based practical validation (Question 1)
- ✅ 9 comprehensive quiz questions (Questions 2-10)
- ✅ Instant feedback with detailed explanations
- ✅ Score tracking across AR + Quiz sections

### **🔷 Digital Certification**
- 🎓 Automatic certificate generation
- 📊 Score-based status (Distinction/Pass/Needs Improvement)
- ⏰ Timestamped certificates
- 💾 PNG export with automatic Gallery save
- 📤 Easy sharing via WhatsApp, Email, etc.

---

## 🛠️ **Technology Stack**

| Component | Technology |
|-----------|-----------|
| **Game Engine** | Unity 2022.3.62f3 LTS |
| **AR Framework** | AR Foundation 5.1.0 |
| **AR Platform** | ARCore (Android) |
| **Language** | C# |
| **UI Framework** | Unity UI + TextMesh Pro |
| **External API** | NASA POWER API |
| **Build Target** | Android (API 24+) |

---

## 🎮 **Usage**

### **Main Training Mode**

1. **Launch App**
   - Grant Camera and Location permissions
   - Point camera at floor/table surface
   - Wait for AR plane detection (white grid)

2. **Place Solar Panel**
   - Tap on detected surface
   - Panel appears at tap location
   - Tap different spot to move panel

3. **Adjust Tilt Angle**
   - Swipe **UP** on panel → Increases tilt
   - Swipe **DOWN** on panel → Decreases tilt
   - Watch real-time angle feedback

4. **Validate Installation**
   - 🟢 **Green "OPTIMAL"**: Tilt within ±3° of target
   - 🟡 **Yellow "ADJUST TILT"**: Outside optimal range
   - Target angle calculated based on GPS location

### **Assessment Mode**

1. **AR Practical (Q1)**
   - Place and adjust panel to optimal angle
   - Click "Check Angle"
   - "NEXT" enables only when correct

2. **Quiz Questions (Q2-Q10)**
   - Answer 9 multiple-choice questions
   - View explanations after each answer
   - Progress through all questions

3. **Results & Certificate**
   - View final score (10 questions total)
   - Click "View Certificate"
   - Download to Gallery

---


## 📦 **Installation**

### **From APK (Recommended)**

1. **Enable Unknown Sources**
   - Settings → Security
   - Enable "Install apps from unknown sources"

2. **Download & Install**
   - Download APK from [Releases](https://github.com/ananya-asa/Solar-AR-Training-MVP/releases/tag/v2.0)
   - Open downloaded file
   - Tap "Install"

3. **Grant Permissions**
   - Camera (required for AR)
   - Location (optional, for GPS data)

### **From Source (Developers)**
```bash
# Clone repository
git clone https://github.com/yourusername/solar-ar-training.git

# Open in Unity 2022.3.62f3
# File → Open Project → Select cloned folder

# Configure AR
# Edit → Project Settings → XR Plug-in Management → Enable ARCore

# Build
# File → Build Settings → Android → Build
```

---

## 🏗️ **Architecture**

### **Scene Structure**
```
📁 Project Root
│
├── 🎬 ARPlacementFinal (Main Training Scene)
│   ├── XR Origin (AR Camera + Tracking)
│   ├── AR Session (ARCore)
│   ├── Placement Manager (Panel spawning)
│   ├── Logic Manager (Tilt calculation + NASA)
│   └── solar_panel 1 (AR Object)
│
└── 🎬 ARAssessment (Quiz & Certification)
    ├── AssessmentManager (Flow controller)
    ├── q1Panel (AR validation UI)
    ├── QuizPanel (MCQ interface)
    ├── ResultPanel (Score display)
    └── CertificatePanel (Certificate UI)
```

### **Core Scripts**

| Script | Purpose | Attached To |
|--------|---------|-------------|
| `UltimateSolarBrain.cs` | Tilt calculation, NASA API | Logic Manager |
| `ManualTilt.cs` | Touch gesture controls | solar_panel 1 |
| `SinglePanelManager.cs` | AR placement logic | Placement Manager |
| `AssessmentFlow.cs` | Quiz orchestration | AssessmentManager |
| `QuizController.cs` | MCQ management | AssessmentManager |
| `CertificateGenerator.cs` | Certificate creation | AssessmentManager |

---

## 🌐 **API Integration**

### **NASA POWER API**
```
Endpoint: https://power.larc.nasa.gov/api/temporal/daily/point
Parameters: ALLSKY_SFC_SW_DWN (Solar irradiance)
Response: JSON with daily solar data
```

**Fallback Formula** (when offline):
```csharp
targetTilt = (|latitude| × 0.76) + 3.1
// Example: Mangaluru (12.9°N) → 12.9 × 0.76 + 3.1 = 12.9°
```

---

## 🔧 **Setup**

### **Prerequisites**
- Unity 2022.3.62f3 or later
- Android SDK (API 24+)
- ARCore supported device

### **Build Instructions**

1. **Open Project**
```bash
   Unity Hub → Add → Select project folder
```

2. **Configure Build Settings**
```
   File → Build Settings
   Platform: Android
   Switch Platform
```

3. **Player Settings**
```
   Minimum API Level: Android 7.0 (API 24)
   Target API Level: Android 13 (API 33)
   XR Plug-in Management: Enable ARCore
```

4. **Build APK**
```
   Build Settings → Build
   Save as: SolarAR-Training-v1.0.apk
```

---

## ⚠️ **Requirements**

### **Device Requirements**
- ✅ Android 7.0 or higher
- ✅ ARCore compatible device
- ✅ 200 MB free storage
- ✅ Camera permission
- ⚡ Internet (optional, for NASA data)

### **Recommended**
- 📱 Android 10+ for best performance
- 📶 Stable internet connection
- ☀️ Well-lit environment for AR

---

## 🐛 **Troubleshooting**

### **AR Not Working**
```
✓ Check device compatibility: https://developers.google.com/ar/devices
✓ Grant Camera permission
✓ Use well-lit environment
✓ Point at textured surfaces (not plain white)
```

### **GPS Takes Too Long**
```
✓ App uses default calculation (12.9°) immediately
✓ GPS upgrades to exact location in background
✓ Works offline with formula-based calculation
```

### **Certificate Not Saving**
```
✓ Grant Storage permission
✓ Check Gallery app for saved image
✓ Location: /storage/emulated/0/Pictures/
```

---

## 🚀 **Roadmap**

### **Phase 1: MVP** ✅ **COMPLETED**
- [x] AR panel placement
- [x] Tilt validation
- [x] NASA API integration
- [x] Quiz system
- [x] Certificate generation

### **Phase 2: Enhancements** 🔜
- [ ] Orientation/Azimuth validation
- [ ] Energy loss calculation (%)
- [ ] Professional UI redesign
- [ ] AR-based mistake scenarios

### **Phase 3: Advanced** 🔮
- [ ] Shadow detection
- [ ] VR training mode
- [ ] Multi-language support
- [ ] Cloud certification

---

## 🤝 **Contributing**

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 **License**

This project was developed as part of an internship program.

**Copyright © 2025 Chainfly.All rights reserved**

---

## 👨‍💻 **Developer**

**[Ananya]**

- GitHub: [@ananya-asa](https://github.com/ananya-asa)


## 🙏 **Acknowledgments**
- **Chainfly** - Project ownership, mentorship, and internship opportunity
- **Unity Technologies** - Game Engine
- **Google ARCore** - AR Platform
- **NASA POWER** - Solar Data API
- **TextMesh Pro** - Typography


---


**⭐ Star this repository if you found it helpful! ⭐**

**Built with ❤️ using Unity & AR Foundation**

</div>

---

**Last Updated**: January 11, 2026  
**Version**: 1.0.0  
**Status**: Production Ready
