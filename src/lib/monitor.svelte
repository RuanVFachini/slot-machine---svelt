<script lang="ts">
import { cubicIn, cubicInOut } from "svelte/easing";

import { tweened } from "svelte/motion";

        const rotateDegre = tweened(0, {
		    duration: 300,
		    easing: cubicInOut
	    });


    function rotate(event: MouseEvent): void {
        if ($rotateDegre == 0) {
            $rotateDegre = -50;
            setTimeout(() => {
                $rotateDegre = 0;
            }, 1000);
        }
        
    };

</script>

<div class="slot-machine">
    <div class="vertical-partition">
        <div class="slot-machine-body">
            <div class="monitor radius-top">
                <div class="screen">
                    <!-- <div class="screen-image-1"></div>
                    <div class="screen-image-2"></div>
                    <div class="screen-image-3"></div> -->
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

    .screen-image-1 {
        flex: 1;
        background-color: crimson;
    }

    .screen-image-2 {
        flex: 1;
        background-color: darkgoldenrod;
    }

    .screen-image-3 {
        flex: 1;
        background-color: darkolivegreen;
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