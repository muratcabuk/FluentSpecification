<div class="mermaid">
graph TD;
CreateIssuesReport-->DotNetCore-Build;
DotNetCore-Build-->Clean;
DotNetCore-Build-->DotNetCore-Restore;
DotNetCore-Build-->Restore;
Clean-->Show-Info;
Clean-->Print-AppVeyor-Environment-Variables;
</div>