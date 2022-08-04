<script lang="ts">
import MachineLever from "./machine-lever.svelte";
import MachineScreen from "./machine-screen.svelte";
import MachineRank from "./machine-rank.svelte";
import type { ScoreItem } from "src/models/models";

    export let level = 15;
    let screen : {randomAll(): void};

    let scoreList: ScoreItem[] = [];

    function random() {
        screen.randomAll();
    }

</script>

<div class="slot-machine">
    <div class="vertical-partition">
        <div class="slot-machine-body">
            <div class="monitor radius-top">
                <MachineScreen bind:screen={screen} bind:level={level}></MachineScreen>
            </div>
            <div class="monitor-body radius-bottom">
                <div class="keyboard radius-bottom">
                    <MachineRank bind:scoreList={scoreList}></MachineRank>
                </div>
            </div>
        </div>
    </div>
    <MachineLever on:rotate={random}></MachineLever>
</div>

<style scoped>

    .vertical-partition {
        display: flex;
        height: fit-content;
        flex-direction: column;
        justify-content: flex-end;
        align-items: flex-end;
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

    .keyboard {
        flex: 1;
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