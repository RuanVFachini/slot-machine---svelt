<script lang="ts">
import { cubicInOut } from "svelte/easing";

import { tweened } from "svelte/motion";
    export let degre = 0;

    const rotateDegre = tweened(0, {
        duration: 300,
        easing: cubicInOut
    });

    $: {
        $rotateDegre = degre;
    }

</script>

<div id="D3Cube" style="transform: rotateX({$rotateDegre}deg)">
    <div id="side1"></div>
    <div id="side2"></div>
    <div id="side3"></div>
    <div id="side4"></div>
</div>

<style scoped>
    #D3Cube {
        width: 110px;
        height: 110px;
        transform-style: preserve-3d;
        margin: auto;
        position: relative;
        transform-style: preserve-3d;
    }

    #D3Cube > div {
        position: absolute;
        width: 100%;
        height: 100%;
        float: left;
        overflow: hidden;
    }

    #side1 {
        transform: rotatex(0deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(13, 241, 32);
    }

    #side2 {
        transform: rotatex(90deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(57, 27, 168);
    }

    #side3 {
        transform: rotatex(180deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(221, 236, 13);
    }

    #side4 {
        transform: rotatex(-90deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(3, 188, 245);
    }


</style>