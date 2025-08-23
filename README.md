# 🎮 Survive with Zombies

> **2D Vampire Survivor 스타일 게임 with ChatGPT AI NPC 시스템**

[![Unity](https://img.shields.io/badge/Unity-2022.3+-black.svg?style=flat&logo=unity)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![OpenAI](https://img.shields.io/badge/OpenAI-GPT--4.0-4129919?style=flat&logo=openai&logoColor=white)](https://openai.com/)

## 📖 프로젝트 개요

**Survive with Zombies**는 전통적인 2D 서바이벌 게임에 **ChatGPT AI NPC 시스템**을 접목한 혁신적인 게임입니다. 플레이어는 NPC와 자연어로 대화하여 게임 내 동료를 생성하고, 그들의 속성을 동적으로 변경할 수 있습니다.

### 🎯 핵심 특징

- 🤖 **AI NPC 시스템**: ChatGPT API를 활용한 실시간 자연어 대화
- 👥 **동적 동료 생성**: 대화를 통해 원하는 스펙의 동료 요청 및 생성
- 🎮 **서바이벌 게임플레이**: 90초 생존 모드 with 점진적 난이도 증가
- 🏘️ **듀얼 씬 시스템**: 마을(Town)과 전투장(Field) 분리
- 💾 **데이터 지속성**: NPC 데이터 JSON 저장/로드 시스템

## 📚 스토리

### 배경 설정

주인공 **조나단**은 한적한 시골 마을의 평범한 농부였습니다. 그는 자연과 대화하며 살아가는 데 익숙한 사람이었죠. 하지만 어느 날, 알 수 없는 바이러스가 확산되어 사람들을 끔찍한 좀비로 변모시켰습니다.

### 생존의 시작

조나단은 농장에서 살아남았습니다. 그의 갈고닦은 농기구 솜씨와 커다란 더블배럴 샷건 덕분에 농장을 지킬 수 있었죠. 이제 그는 좀비로 변한 세상에서 생존하는 데 필요한 모든 것을 활용해야 합니다.

### 희망의 발견

조나단은 우연히 이 전염병을 실수로 퍼트리고 만 과학자 '닥터 비'를 발견하게 됩니다. 닥터 비는 마을에서 살아남은 유일한 인간이었고, 그녀의 지식은 바이러스를 이해하고 마침내 이를 멈추는 데 중요한 역할을 해줄 것입니다. 더욱이, 그녀는 좀비들의 부속을 이용하여 조나단에게 도움이 될 새로운 동료를 만들어줄 수 있었습니다.

### 임무

이제 조나단의 임무는 과학자를 보호하며, 그녀가 바이러스를 치료할 방법을 찾도록 돕는 것입니다. 이를 통해 그는 인류의 생존을 확보하고, 자신의 농장을 다시 안전한 피난처로 만들 수 있을 것입니다.

## 🎮 게임플레이

### 🏘️ 마을 (Town Scene)
- **정비 공간**: 던전에 가기 전 준비
- **동료 영입**: AI NPC와 대화하여 동료 생성
- **동료 교감**: 영입한 동료와 마을에서 상호작용
- **무기 선택**: 시작 무기 선택 후 스테이지 진입

### ⚔️ 전투장 (Field Scene)
- **90초 생존 모드**: 시간이 지날수록 적이 강해짐
- **아이템 드롭**: 좀비 처치 시 부속품 또는 골드 획득
- **동료 동행**: 최대 3명의 동료와 함께 전투
- **속성 시스템**: 동료에 따른 다양한 공격 속성

### 👥 동료 시스템

#### 속성 차트
| 공격력 | 사정거리 | 공격 속도 |      속성        |
|-------|--------|----------|------------------|
|  +1   |   +1   |    +1    | **지속** (불, 독) |
|  +2   |   +2   |    +2    | **연쇄** (번개)   |
|  +3   |   +3   |    +3    | **유도** (호밍)   |
|  +4   |   +4   |    +4    | **폭발** (범위)   |

#### 속성 설명
- **지속**: 초당 % 지속 데미지
- **연쇄**: 피격되는 적 주변의 적에게 추가 데미지 (연쇄 횟수 증가 시 데미지 감소)
- **유도**: 가까운 적에게 유도되는 탄환 발사
- **폭발**: 느리지만 범위 내 모든 적에게 데미지

## 🚀 설치 및 실행

### 다운로드
[게임 다운로드 링크](https://drive.google.com/drive/folders/1FVL0y2wKf8bBCjeeLM4x5rOhgrzkBAiw?usp=sharing) (Google Drive)에서 `ZombieSurvivor.zip`를 다운받으세요.

### 실행 방법
1. Zip 파일을 압축 해제
2. `ZombieSurvivor` 폴더 안의 `Survive_with_Zombies.exe` 실행
3. 게임 시작 버튼 클릭

## 🎯 사용법

### NPC 대화 시스템
1. 캐릭터 상단의 NPC에게 접근
2. **Space 바**를 눌러 대화 시작
3. 채팅으로 질문 (예: "안녕, 넌 누구야?", "넌 뭘 할 수 있어?")
4. **X 버튼**으로 채팅 종료

### 게임 진행
1. 캐릭터 우측으로 이동하여 Field Scene으로 전환
2. 게임 시작 버튼 클릭
3. 90초 동안 생존하며 좀비 처치
4. 마을로 돌아와 동료 영입 및 아이템 교환

## 🔧 기술 스택

### 게임 엔진
- **Unity 2022.3+**: 2D 게임 개발
- **C#**: 스크립팅 언어
- **Unity Input System**: 입력 처리

### AI 시스템
- **OpenAI GPT-3.5-turbo**: 자연어 처리
- **JSON**: NPC 데이터 저장
- **실시간 채팅**: WebSocket 기반 통신

### 게임 시스템
- **Object Pooling**: 성능 최적화
- **ScriptableObject**: 데이터 관리
- **Scene Management**: 씬 전환
- **Animation System**: 2D 애니메이션

## 📁 프로젝트 구조

```
Assets/
├── 1. Scripts/              # C# 스크립트
│   ├── GameManager.cs       # 게임 전체 관리
│   ├── Player.cs           # 플레이어 컨트롤
│   ├── ChatGPT_NPC.cs      # AI NPC 시스템
│   ├── NPCManager.cs       # NPC 데이터 관리
│   ├── Enemy.cs            # 적 AI
│   ├── Weapon.cs           # 무기 시스템
│   ├── Spawner.cs          # 적 스폰 시스템
│   └── NPCs/               # NPC 데이터 파일
├── 2. Animations/           # 애니메이션
├── 3. Scenes/              # 씬 파일
│   ├── Town.unity          # 마을 씬
│   └── Field.unity         # 전투 씬
├── 4. Tiles/               # 타일맵
├── 5. Fonts/               # 폰트
├── 6. Audio/               # 오디오
└── 0. Prefabs/             # 프리팹
```

## 🎨 주요 기능

### ✅ 구현 완료
- [x] NPC와의 대화 (GPT API 3.5 --> 4.0 변경)
- [x] 90초 게임 플레이
- [x] 무기 업그레이드, 버프 추가
- [x] 체력 회복 시스템
- [x] 각 몬스터별 다른 화폐 획득
- [x] 동적 NPC 속성 변경
- [x] JSON 기반 데이터 저장/로드

### 🚧 개발 중
- [ ] 추가 동료 속성 시스템
- [ ] 고급 무기 시스템
- [ ] 마을 꾸미기 요소
- [ ] 펫 시스템

## 🎵 에셋 출처

- [Undead Survivor Assets Pack](https://assetstore.unity.com/packages/2d/undead-survivor-assets-pack-238068)

## 📚 개발 참고 자료

- [골드메탈 뱀서 게임 제작](https://www.youtube.com/watch?v=MmW166cHj54&list=PLO-mt5Iu5TeZF8xMHqtT_DhAPKmjF6i3x)
- [골드메탈 탑다운 2D RPG](https://www.youtube.com/watch?v=JY-KFx3OsJo&list=PLO-mt5Iu5TeYfyXsi6kzHK8kfjPvadC5u)
- [MMORPG 개발](https://www.inflearn.com/roadmaps/355)
- [C# 강의](https://www.youtube.com/watch?v=u8T_h3Yn-xQ&list=PL4SIC1d_ab-Y-bBKojxhtFWwNpawMM1h5)

---

> **Notion 기획서**: [Survive with Zombies 개발 문서](https://www.notion.so/Survive-with-Zombies-7777f8f763444e2c8355eedd643acdfb)
