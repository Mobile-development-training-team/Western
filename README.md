# 移动开发实训项目
更新日志
---
1. 2020.5.27 场景隔三秒自动生成敌人，敌人死亡隔五秒消失，主角弹种增加实体弹，射出10秒后或遇敌消失，镭射移除（脚本还在）。存在问题：子弹发射会在上一次开枪位置射出、连射偶尔会把前面的子弹回收、生成的敌人动画状态机不完整，死亡再被攻击任有判定且会站立，站位有问题，不在一条直线上
2. 2020.5.31 完成双手剑敌人动画状态机和通用型EnemyController，敌人朝勇者移动，勇者进入攻击范围则攻击，攻击碰撞成功则勇者受伤，敌人三秒后死亡（可移去此段代码），死亡三秒后尸体回收，修复敌人受攻击后无法行动问题和死亡再被攻击会站立问题，修复敌人死亡后勇者踩到敌人武器会受伤问题，修复敌人死亡后仍有判定问题（身体倒地，倒地位置上方仍有站立时的判定，即空气墙，尸体会被推走，子弹会被挡住）。修改braveController，修复勇者受攻击后无法操作问题。修改BulletController，修复子弹连射把前面的子弹回收问题。完成简单敌人控制器EnemiesManager01，共三波敌人（已设成三波敌人循环，可去循环），第一波：三秒后左边生成双手剑敌人一个；第二波：四秒后两边各生成一个双手剑敌人；第三波：五秒后两边和中间各生成一个双手剑敌人；修复敌人站位不在一条直线问题。
2. 2020.6.2 修改EnemyController和BraveController，修改敌人和勇者受攻击动作启动方式，增加勇者死亡(void Death())/复活(void Revive())/受攻击(void GetHit())/跳跃(void Jump())动作控制函数，增加勇者跳跃功能。弃用GUI按钮控制，导入手柄组件使用手柄按钮控制，方向盘控制移动方向，手柄推半为行走，手柄推满为奔跑；X：攻击；Y：回旋砍/换弹；B：换武器；A：跳跃（四个按钮图片样式可更改）
3. 2020.6.4 导入ModularRPGHeroesPolyArt组件，更换勇者、双手剑敌人外貌及武器样式。勇者增加法杖武器及动作（远程发射魔法球，技能：咏唱后同时发射三个魔法球），增加弹种魔法球（法杖专用，射程短且弹道速度慢）。修改子弹射程（即子弹回收时间缩短至3秒）。