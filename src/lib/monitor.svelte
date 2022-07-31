<script lang="ts">
import { cubicIn, cubicInOut } from "svelte/easing";

import { tweened } from "svelte/motion";
import Dice from "./dice.svelte";
import getRandom from "../utils/random.utils";

    export let level = 15;

    let dice1 = 0;
    let dice2 = 0;
    let dice3 = 0;

    const rotateDegre = tweened(0, {
        duration: 300,
        easing: cubicInOut
    });

    function random(dice: any): number {
        return dice + (getRandom(level) * 90);
    }

    function randomAll(): void {
        Promise.all([
            dice1 = random(dice1),
            dice2 = random(dice2),
            dice3 = random(dice3),
        ]);
    }

    function rotate(event: MouseEvent): void {
        if ($rotateDegre == 0) {
            $rotateDegre = -50;
            setTimeout(() => {
                $rotateDegre = 0;

                randomAll();
            }, 200);
        }
        
    };

</script>

<div class="slot-machine">
    <div class="vertical-partition">
        <div class="slot-machine-body">
            <div class="monitor radius-top">
                <div class="screen">
                    <div class="screen-image">
                        <Dice degre={dice1}></Dice>
                    </div>
                    <div class="screen-image">
                        <Dice degre={dice2}></Dice>
                    </div>
                    <div class="screen-image">
                        <Dice degre={dice3}></Dice>
                    </div>
                </div>
            </div>
            <div class="monitor-body radius-bottom">
                <div class="keyboard radius-bottom">

                </div>
            </div>
        </div>
    </div>
    <div class="vertical-partition bottom-100">
        <div class="vertical-partition"  >
            
            <div id="lever" on:click={rotate}  class="start-lever" style="transform: perspective(15cm) rotateX({$rotateDegre}deg); ">

            </div>
            <div class="distance-piece">

            </div>
        </div>
    </div>
</div>

<style scoped>

    .bottom-100 {
        margin-bottom: 100px;
    }

    .screen-image {
        flex: 1;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100%;
    }

    .distance-piece  {
        height: 30px;
        width: 30px;
        z-index: -1000;
        background-color: yellow;
    }

    .vertical-partition {
        display: flex;
        height: fit-content;
        flex-direction: column;
        justify-content: flex-end;
        align-items: flex-end;
    }

    .start-lever {
        width: 20px;
        height: 180px;
        transform-origin: 50% 120%;
        background-color: chartreuse;
    }

    .slot-machine {
        display: flex;
        margin-bottom: 20px;
        align-items: flex-end;
    }

    .slot-machine-body {
        height: 650px;
        width: 400px;
    }

    .monitor {
        height: 340px;
        max-width: 100%;
        display: flex;
        /* border: 30px solid gray; */
        /* box-shadow:0px 0px 0px 15px red inset; */
        outline: 20px solid gray;
        outline-offset: -20px;
    }

    .monitor-body {
        height: 250px;
        max-width: 100%;
        display: flex;
        background-color: aqua;
        border: 1px solid gray;
    }

    .screen {
        /* background-color: blueviolet; */
        /* margin: 30px; */
        flex: 1;
        display: flex;
        padding: 20px;
    }

    .keyboard {
        flex: 1;
        margin: 20px;
        background-color: brown;
    }

    .radius-top {
        border-top-left-radius: 20px;
        border-top-right-radius: 20px;
        
    }

    .radius-bottom {
        border-bottom-left-radius: 10px;
        border-bottom-right-radius: 10px;
    }
</style>