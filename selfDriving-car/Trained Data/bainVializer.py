import pygame
import math

from pygame import draw

WIDTH,HEIGHT=800,800

def getColor(x):
    if x>1:
        x=1
    if x<-1:
        x=-1
    y=int(((x+1)*255)/2)
    return y
def sigmoid(x):
    return 1/(1+(math.exp(-x)))
class Node:
    def __init__(self,x,y,screen):
        self.screen=screen
        self.x=x
        self.y=y
        self.redius=20
        self.color=(255,255,0)
        self.borderColor=(255,255,255)
        self.borderSize=1
    
    def render(self):
        pygame.draw.circle(self.screen,self.borderColor,(self.x,self.y),self.redius+self.borderSize)
        pygame.draw.circle(self.screen,self.color,(self.x,self.y),self.redius)

class connection:
    def __init__(self,nodeA,nodeB,screen,weight):
        self.x1=nodeA.x
        self.y1=nodeA.y
        self.x2=nodeB.x
        self.y2=nodeB.y
        self.weight=weight
        self.screen=screen
        color=getColor(weight)
        self.color=(color,color,color)

    def render(self):
        pygame.draw.line(self.screen,self.color,(self.x1,self.y1),(self.x2,self.y2),5)

screen=pygame.display.set_mode((WIDTH,HEIGHT))
nodes=[]
connections=[]
w1=open("W1.txt","r").read().split("\n")
w2=open("W2.txt","r").read().split("\n")
l1=4;
l2=4
l3=2
xCounter=50
yCOunter=50
gapX=350
gapY=225
layer1=[]
layer2=[]
layer3=[]
for i in range(l1):
    layer1.append(Node(xCounter,yCOunter,screen))
    yCOunter+=gapY
xCounter+=gapX
yCOunter=50
for i in range(l2):
    layer2.append(Node(xCounter,yCOunter,screen))
    yCOunter+=gapY
xCounter+=gapX
yCOunter=250

for i in range(l3):
    layer3.append(Node(xCounter,yCOunter,screen))
    yCOunter+=gapY
xCounter+=gapX

layerCounter=0
for line in w1:
    line=line.split("\t")
    weightCounter=0
    for weight in line:
        w=float(weight)
        connections.append(connection(layer1[layerCounter],layer2[weightCounter],screen,w))
        weightCounter+=1
    layerCounter+=1

layerCounter=0
for line in w2:
    line=line.split("\t")
    weightCounter=0
    for weight in line:
        w=float(weight)
        connections.append(connection(layer2[weightCounter],layer3[layerCounter],screen,w))
        weightCounter+=1
    layerCounter+=1


def draw():
    for c in connections:
        c.render()
    for n in layer1:
        n.render()
    for n in layer2:
        n.render()
    for n in layer3:
        n.render()
    pygame.display.update()

def update():
    pygame.event.get()

while True:
    draw()
    update()


