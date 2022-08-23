<script lang="ts">
import { cubicInOut } from "svelte/easing";
import fruit1 from "./../assets/images/fruit-1.png";
import fruit2 from "./../assets/images/fruit-2.png";
import fruit3 from "./../assets/images/fruit-3.png";
import fruit4 from "./../assets/images/fruit-4.png";
import fruit5 from "./../assets/images/fruit-5.png";
import fruit6 from "./../assets/images/fruit-6.png";
import fruit7 from "./../assets/images/fruit-7.png";
import fruit8 from "./../assets/images/fruit-8.png";

import { tweened } from "svelte/motion";
    export let degre = 0;

    const rotateDegre = tweened(0, {
        duration: 1000,
        easing: cubicInOut
    });

    $: {
        $rotateDegre = degre;
    }

</script>
<div id="D3Cube" style="transform: rotateX({$rotateDegre}deg)">
    <div id="side1" class="dice-card">
        <img height="96" width="96" src={fruit1} alt="fruit-1"/>
    </div>
    <div id="side2" class="dice-card">
        <img height="96" width="96" src={fruit2} alt="fruit-2"/>
    </div>
    <div id="side3" class="dice-card">
        <img height="96" width="96" src={fruit3} alt="fruit-3"/>
    </div>
    <div id="side4" class="dice-card">
        <img height="96" width="96" src={fruit4} alt="fruit-4"/>
    </div>
    <div id="side5" class="dice-card">
        <img height="96" width="96" src={fruit5} alt="fruit-5"/>
    </div>
    <div id="side6" class="dice-card">
        <img height="96" width="96" src={fruit6} alt="fruit-6"/>
    </div>
    <div id="side7" class="dice-card">
        <img height="96" width="96" src={fruit7} alt="fruit-7"/>
    </div>
    <div id="side8" class="dice-card">
        <img height="96" width="96" src={fruit8} alt="fruit-8"/>
    </div>
</div>

<style scoped>
    .dice-card {
        border: 1px solid black;
        display:flex;
        justify-content: center;
        align-items: center;
        background-color: rgb(255, 255, 255);
    }
    #D3Cube {
        width: 110px;
        height: 100px;
        transform-style: preserve-3d;
        /* margin: auto; */
        display: flex;
        justify-content: center;
        position: relative;
        transform-style: preserve-3d;
    }

    #D3Cube > div {
        position: absolute;
        width: 110px;
        height: 100%;
        align-items: center;
        overflow: hidden;
    }

    #side1 {
        transform: rotatex(0deg) translateX(0px) translateY(0px) translateZ(122px);
    }

    #side2 {
        transform: rotatex(45deg) translateX(0px) translateY(0px) translateZ(122px);
    }

    #side3 {
        transform: rotatex(90deg) translateX(0px) translateY(0px) translateZ(122px);
    }

    #side4 {
        transform: rotatex(135deg) translateX(0px) translateY(0px) translateZ(122px);
    }

    #side5 {
        transform: rotatex(180deg) translateX(0px) translateY(0px) translateZ(122px);
    }

    #side6 {
        transform: rotatex(225deg) translateX(0px) translateY(0px) translateZ(122px);
    }

    #side7 {
        transform: rotatex(270deg) translateX(0px) translateY(0px) translateZ(122px);
    }

    #side8 {
        transform: rotatex(315deg) translateX(0px) translateY(0px) translateZ(122px);
    }


</style>