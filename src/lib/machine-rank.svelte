<script lang="ts">
import type { ScoreItem } from "src/models/models";

export let scoreList: ScoreItem[];

</script>

<div class="score">
<table class="score-table">
    <thead>
        <th>Name</th>
        <th>Score</th>
    </thead>
    <tbody>
        {#each scoreList as item, index}
        {#if index < 3}
            <tr>
                <td>{item.name}</td>
                <td>{item.score}</td>
            </tr>
        {/if}
	    {/each}
        
    </tbody>
</table>
</div>

<style scoped>
.score {
    flex: 1;
    margin: 20px;
    border-radius: 5px;
    padding: 5px;
    height: calc(100% - 40px - 10px);
    background-color: brown;
}
.score-table {
    width: 100%;
}

th {
    text-align: start;
}
</style>