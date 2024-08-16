# KGA_OOPConsoleProject
 TEXT_RPG_02
# TEXT_RPG
 >C# 콘솔 프로젝트 02 입니다.
 
# 프로젝트 정보
* 게임장르 : TEXT RPG 02(미로탐색)
* 제작인원 : 1인(개인 프로젝트)
* 제작기간 : 5일
* 사용엔진 및 언어 : VisualStudio2020, C#

# 프로젝트 계획
### 타임라인
![타임라인-001 (2)](https://github.com/user-attachments/assets/667580d9-e007-4ebe-a7c9-cfee55a04d37)

### 클래스 다이어그램
![Text_RPG_02 drawio](https://github.com/user-attachments/assets/58eac377-2b76-435f-9314-c4d104388ced)

---

# 플레이 화면 및 특징

   ### 1. 타이틀 화면 및 미로 맵 랜덤 생성(DFS - Recursive_Backtracking)
![시작 타이틀 화면](https://github.com/user-attachments/assets/713f38e5-4d85-4f7f-9914-4d76a9d4e4e4)

   ![스크린샷 2024-08-12 222721](https://github.com/user-attachments/assets/84c81fd5-efed-4a69-9fdc-0e2cb5ba1026)
   
      (DFS를 이용하여 미로를 만들었습니다.)
---

  ### 2. 캐릭터 이동 및 장애물
![캐릭터 이동](https://github.com/user-attachments/assets/c7e0b32e-c6f9-4782-9e4c-68e557a732b2)
![캐릭터 장애물 이동](https://github.com/user-attachments/assets/b24c6820-6711-44c6-a5d2-16aa1d0be77d)

 
 > 캐릭터의 이동과 장애물을 생성.(장애물은 캐릭터가 앞으로 움직일 수 있다. 단 벽으로 밀었을시 터진다.)
---

  ### 3. 아이템 획득 및 사용
![아이템 획득](https://github.com/user-attachments/assets/46b9324b-3871-42d5-9ace-2e9f5652d3a5)
 > 장애물이 터질시 아이템을 획득할 수 있다.

![이동포션 사용](https://github.com/user-attachments/assets/5305c88f-e6cb-449b-899f-5222532d3726)
> (1. 이동포션 = 10의 이동 게이지 획득(최대범위 초과시 최대범위까지만 획득))

![맵 리서치](https://github.com/user-attachments/assets/e69040b5-b4e2-4b11-9001-0a47e092b395)
> (2. 맵 리서치 = 미로의 최단경로를 보여준다.(BFS사용))

![아이템 사용 점프](https://github.com/user-attachments/assets/f4bafe92-da60-44cb-b6f4-b52b234c2639)
> (3. 점프 = 벽을 건너뛸 수 있게해 준다.)
---

  ### 4. 스테이지
![스테이지](https://github.com/user-attachments/assets/47b7de9d-627b-45ef-8482-3eaa3c7add73)

> 3스테이지까지 구성 했으며, 스테이지가 올라갈수록 크기가 커진다.
> 
> 3스테이지에서는 장애물이 움직일때마다 보였다가 사라졌다가를 반복시켰다.
 ---

  ### 5. 게임 종료 및 리셋
![스테이지 종료 후 점수 및 리셋](https://github.com/user-attachments/assets/366da41b-5262-4fb8-a63b-8d7500d2ded9)

> 3스테이지 종료 후 최종 점수 합산이 나온다.
> 그리고 게임이 종료 후 다시 플레이시 리셋이된 상태로 시작이 가능하다.
