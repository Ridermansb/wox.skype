Write-Host -ForegroundColor DarkBlue "Upload coverage report ..."

Add-Content "$env:USERPROFILE\.git-credentials" "https://$($env:GITHUB_ACCESS_TOKEN):x-oauth-basic@github.com/Ridermansb/wox.skype.git"

ls

git clone -b gh-pages https://github.com/Ridermansb/wox.skype.git gh-pages

cd gh-pages

. ..\$env:reportPath  -reports:..\$env:coverResultFile -targetdir:coverage

git add .

git commit -am "Update coverage code report"

git push origin gh-pages
