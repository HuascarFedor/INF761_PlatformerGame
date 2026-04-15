# INF761 - Desarrollo de Videojuegos

Proyecto de la materia **INF761 - Desarrollo de Videojuegos** de la carrera de Ingeniería Informática, Universidad Autónoma Tomás Frías (UATF), Potosí, Bolivia.

Este repositorio contiene el proyecto desarrollado en **Unity 6** como parte de las guías de laboratorio del curso.

---

## Requisitos previos

- **Unity 6.3** o superior (instalar desde [Unity Hub](https://unity.com/download))
- **Git** instalado ([descargar aquí](https://git-scm.com/downloads))
- **Git LFS** (opcional, solo si el proyecto usa archivos grandes)

---

## Instalación

### 1. Clonar el repositorio

```bash
git clone https://github.com/USUARIO/REPOSITORIO.git
```

Si el proyecto usa Git LFS, asegúrate de tenerlo instalado antes de clonar:

```bash
git lfs install
git clone https://github.com/USUARIO/REPOSITORIO.git
```

### 2. Abrir el proyecto en Unity

1. Abrir **Unity Hub**.
2. Hacer clic en **Open → Add project from disk**.
3. Seleccionar la carpeta del repositorio clonado.
4. Unity detectará la versión del editor y regenerará las carpetas necesarias (`Library/`, `Temp/`, `Obj/`).

> La primera apertura puede tardar varios minutos mientras se reimportan los assets y se descargan los paquetes del Package Manager.

### 3. Verificar que todo funcione

- Si aparecen errores de scripts o referencias rotas, ir a **Assets → Reimport All**.
- Verificar que la versión del editor coincida con la utilizada en el proyecto.

---

## Estructura de tags

El repositorio está organizado en **4 tags**, cada uno correspondiente al avance de una guía de laboratorio:

| Tag | Guía | Descripción |
|-----|------|-------------|
| `guia-5` | Guía 5 | *Tile Palette, creación de tilesets, Tilemap Collider 2D, capas de tilemap (fondo, suelo, decoración), Cinemachine básico, Raycast para detección de suelo. Mini-juego: nivel de plataformas recorrible.* |
| `guia-6` | Guía 6 | *Sprite sheets, Animation clips, Animator Controller, transiciones con parámetros, Coyote Time, salto variable. Evolución: personaje animado con movimiento fluido.* |
| `guia-7` | Guía 7 | *Enemigos patrulleros (waypoints), monedas coleccionables, sistema de vidas con iframes, trampas (pinchos, plataformas móviles), ScriptableObjects para datos de items.* |
| `guia-8` | Guía 8 | *SceneManager, Canvas completo (HUD, menú principal, game over), botones, DontDestroyOnLoad, transición entre niveles. Resultado: juego de plataformas completo y jugable.* |

## Docente

M. Sc. Huáscar Fedor Gonzales Guzmán

---

## Licencia

Proyecto con fines académicos. Universidad Autónoma Tomás Frías, Potosí, Bolivia.
