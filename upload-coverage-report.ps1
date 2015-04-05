Write-Host -ForegroundColor DarkBlue "Upload coverage report ..."

Add-Content "$env:USERPROFILE\.git-credentials" "https://$($env:GITHUB_ACCESS_TOKEN):x-oauth-basic@github.com/Ridermansb/wox.skype.git"

git clone -b gh-pages https://github.com/Ridermansb/wox.skype.git gh-pages

cd gh-pages

Move-Item coverage\* gh-pages\coverage -Force

git add .

git commit -am "Update coverage code report"

git push origin gh-pages
