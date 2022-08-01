<script lang="ts">
    import { createEventDispatcher } from 'svelte';
import { cubicInOut } from 'svelte/easing';
import { tweened } from 'svelte/motion';
    let dispatch = createEventDispatcher();

    const rotateDegre = tweened(0, {
        duration: 300,
        easing: cubicInOut
    });

    function rotate(): void {

        if ($rotateDegre == 0) {
            $rotateDegre = -50;
            setTimeout(() => {
                $rotateDegre = 0;

                dispatch('rotate', null);
            }, 200);
        }
    }
</script>

<div class="vertical-partition bottom-100">
    <div class="vertical-partition"  >
        
        <div id="lever" on:click={rotate}  class="start-lever" style="transform: perspective(15cm) rotateX({$rotateDegre}deg); ">

        </div>
        <div class="distance-piece">

        </div>
    </div>
</div>

<style scoped>
    .bottom-100 {
        margin-bottom: 100px;
    }

    .distance-piece  {
        height: 30px;
        width: 30px;
        z-index: -1000;
        background-color: yellow;
    }

    .start-lever {
        width: 20px;
        height: 180px;
        transform-origin: 50% 120%;
        background-color: chartreuse;
    }

    .vertical-partition {
        display: flex;
        height: fit-content;
        flex-direction: column;
        justify-content: flex-end;
        align-items: flex-end;
    }
</style>